#include <Windows.h>
#include <fstream>
#include "../src/hashlibpp.h"
using namespace std;

HANDLE inputMutex, outputMutex;
ifstream *infile;
ofstream *outfile;
hashwrapper *md5 = new md5wrapper();
hashwrapper *sha1 = new sha1wrapper();
char* block;
int sizeBlock = 0;
char* hashType;

void Thread(void* pParams)
{
		block = new char[sizeBlock];
		while (!infile->eof())
		{
			WaitForSingleObject(inputMutex, INFINITE);
				infile->read((char*)(block), sizeBlock);
			ReleaseMutex(inputMutex);
			
			WaitForSingleObject(outputMutex, INFINITE);
				if (!strcmp(hashType, "md5"))	*outfile << md5->getHashFromString(block) << endl;
				else	*outfile << sha1->getHashFromString(block) << endl;
			ReleaseMutex(outputMutex);
		}
}

int main ( int argc, char **argv)
{
	char outName[300];
	int countThreads = 1;
	if (!strcmp(argv[4], "sync")) countThreads = 3;
	sizeBlock = atoi(argv[2]);
	hashType = argv[3];

	HANDLE *threads = new HANDLE[countThreads];
	inputMutex = CreateMutex(NULL, FALSE, (LPCTSTR)"file input");
	outputMutex = CreateMutex(NULL, FALSE, (LPCTSTR)"file output");

	strcpy(outName, argv[1]);
	strcat(outName, ".hashes_");
	strcat(outName, argv[3]);

	infile = new ifstream(argv[1], ios::binary | ios::in);
	outfile = new ofstream(outName, ios::out);

	for (int i = 0; i < countThreads; i++) threads[i] = CreateThread(NULL, 0, (LPTHREAD_START_ROUTINE)Thread, NULL, 0, NULL);
	WaitForMultipleObjects(countThreads, threads, TRUE, INFINITE);

	infile->close();
	outfile->close();
	CloseHandle(inputMutex);
	CloseHandle(outputMutex);
	for (int i = 0; i < countThreads; i++) CloseHandle(threads[i]);
	delete threads;
	delete block;
	delete md5;
	delete sha1;

	return 0;
}
