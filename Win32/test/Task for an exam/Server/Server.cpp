	// Server.cpp : Defines the entry point for the console application.
	//

#include "stdafx.h"
#include <windows.h>  
#include <tchar.h> 
#include <stdlib.h>
#include <iostream>

#define FILENAME L"file.txt"

using namespace std;

BOOL CreateFile();


HANDLE hFile, hFileMap;
LPINT lpstFile;
HANDLE hMutex;

int number;
char BUFF;

int main()
{

	if (CreateFile())
	{
		while (1)
		{
			if (lpstFile[0] == -555) 
			{
				break;
			}

			hMutex = CreateMutex(NULL, FALSE, L"Mutex");
			WaitForSingleObject(hFile, INFINITY);
			cout << (int)lpstFile;
			printf_s(" ");
			cout << (int)lpstFile[0];
			getchar();
			CloseHandle(hMutex);
		}
	}
	CloseHandle(hFileMap);
	CloseHandle(hFile);
	
	return 0;
}



BOOL CreateFile()
{

	if (!(hFileMap = OpenFileMapping(FILE_MAP_READ, FALSE, FILENAME)))
	{
		printf_s("Can't open file map!");
		getchar();
		return FALSE;
	}
	lpstFile = (LPINT)MapViewOfFile(hFileMap, FILE_MAP_READ, 0, 0, 0);



	return TRUE;
}



