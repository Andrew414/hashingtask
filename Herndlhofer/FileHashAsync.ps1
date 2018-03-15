  param(
    [Parameter(Mandatory)]
    [string]$FilePath,
    [Parameter(Mandatory)]
    [int]$BlockSize,
    [Parameter(Mandatory = $true)]
    [ValidateSet('SHA1', 'SHA256', 'SHA384', 'SHA512', 'MD5', 'MACTripleDES', 'RIPEMD160')]
    [String]$Algorithm,
    [ValidateSet([bool]$true, [bool]$false)]
    $isAsync)
function split($inFile, [Int32] $bufSize)
{
  $stream = [System.IO.File]::OpenRead($inFile)
  $chunkNum = 0
  $barr = New-Object -TypeName byte[] -ArgumentList $bufSize
  $directory = New-Item -Path "$((Get-Item  $inFile).Directory)\tmp" -ItemType directory

  while( $bytesRead = $stream.Read($barr,0,$bufSize))
  {
    $outFile = "$directory\$((Get-Item  $inFile).Name).$chunkNum"
    $ostream = [System.IO.File]::OpenWrite($outFile)
    $ostream.Write($barr,0,$bytesRead)
    $ostream.close()
    Write-Output -InputObject "wrote $outFile"
    $chunkNum += 1
  }
  return $chunkNum
}
function HashAsync
{
  param(
    [Parameter(Mandatory)]
    [string]$FilePath,
    [Parameter(Mandatory)]
    [int]$BlockSize,
    [Parameter(Mandatory = $true)]
    [ValidateSet('SHA1', 'SHA256', 'SHA384', 'SHA512', 'MD5', 'MACTripleDES', 'RIPEMD160')]
    [String]$Algorithm,
    [ValidateSet([bool]$true, [bool]$false)]
    $isAsync)
  Measure-Command -Expression {
    try
    {
      if (!$isAsync)
      {
        $isAsync = $true
      }
      [System.Convert]::ToBoolean($isAsync)
      if (!(($BlockSize -ne 0) -and (($BlockSize -band ($BlockSize - 1)) -eq 0)))
      {
        throw [System.IO.Exception] 'blocksize is not power of 2.'
        return
      }
      if (!($BlockSize -lt 131072 -and $BlockSize -gt 1024))
      {
        throw [System.IO.Exception] 'blocksize is not inside the limits.'
        return
      }
      $chunkNum = split -inFile $FilePath  -bufSize $BlockSize
      $files = $($chunkNum.Count-1)
      $outFile = New-Item -Path "$((Get-Item $FilePath).FullName).hash_$Algorithm" -ItemType file
      if ($isAsync -eq $true)
      {
        $RunspacePool = [runspacefactory]::CreateRunspacePool(1, $env:NUMBER_OF_PROCESSORS + 1)
        $RunspacePool.Open()
        $runspaces = @()
        0..$files | ForEach-Object -Process {
          $PowerShell = [PowerShell]::Create() 
          $PowerShell.RunspacePool = $RunspacePool
          [void]$PowerShell.AddScript({
              Param (
                [string]$FilePath,
                [string]$Algorithm,
              [int]$i)
              return Get-FileHash -Path "$((Get-Item  -Path $FilePath).Directory.FullName)\tmp\$((Get-Item  -Path $FilePath).Name).$i"  -Algorithm $Algorithm | Select-Object -ExpandProperty Hash
          })
          [void]$PowerShell.AddArgument($FilePath)
          [void]$PowerShell.AddArgument($Algorithm)
          [void]$PowerShell.AddArgument($_)
          $runspaces += [PSCustomObject]@{
            Pipe   = $PowerShell
            Status = $PowerShell.BeginInvoke()
          }
        }
        $runspaces | ForEach-Object -Process {
          $_.Pipe.EndInvoke($_.Status) | Add-Content  $outFile
          $_.Pipe.Dispose()
        }
        $RunspacePool.Close()
        $RunspacePool.Dispose()
      }
      else 
      {
        for ($i = 0; $i -lt $files; $i += 1)
        {
          Get-FileHash -Path "$((Get-Item  -Path $FilePath).Directory.FullName)\tmp\$((Get-Item  -Path $FilePath).Name).$i"  -Algorithm $Algorithm |
          Select-Object -ExpandProperty Hash |
          Add-Content  $outFile
        }
      }
    }
    finally
    {
      Remove-Item -Path "$((Get-Item  -Path $FilePath).Directory.FullName)\tmp" -Recurse -Force
    }
  }
}
      if (!$isAsync)
      {
        $isAsync = $true
      }
HashAsync -FilePath $FilePath -BlockSize $BlockSize -Algorithm $Algorithm -isAsync $isAsync
