	// Client.cpp : Defines the entry point for the console application.
	//

	#include "stdafx.h"
	#include <windows.h>  
	#include <tchar.h> 
	#include <stdlib.h>
	#include <iostream>

	#define FILENAME L"file.txt"

	using namespace std;

	BOOL CreateFile();

	DWORD dwFileSize;
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
				hMutex = CreateMutex(NULL, FALSE, L"Mutex");
				WaitForSingleObject(NULL, INFINITY);

				cin >> number;
				lpstFile[0] = number;
				printf_s(" ");
				cout << (int)lpstFile;
				printf_s(" ");
				cout << (int)lpstFile[0];

				CloseHandle(hMutex);
			}
		}
		CloseHandle(hFileMap);
		CloseHandle(hFile);

		return 0;
	}


	BOOL CreateFile()
	{
	
		if (!(hFile = CreateFile(FILENAME, GENERIC_WRITE | GENERIC_READ, 0, NULL, OPEN_EXISTING, FILE_ATTRIBUTE_NORMAL, NULL)))
		{
			printf_s("Can't open file!");
			getchar();
			return FALSE;
		}
		else
		{
			dwFileSize = GetFileSize(hFile, NULL);
			if (!(hFileMap = CreateFileMapping(hFile, NULL, PAGE_READWRITE, 0, dwFileSize + sizeof(WCHAR), FILENAME)))
			{
				printf_s("Can't create file map!");
				getchar();
				return FALSE;
			}
			lpstFile = (LPINT)MapViewOfFile(hFileMap, FILE_MAP_WRITE, 0, 0, 0);
			TCHAR szCmdLine[] = _T("D:\\POIS\\ÎÀÏßÂÓ\\C#\\Labs\\Win32\\test\\Task for an exam\\Debug\\Server.exe");
			ShellExecute(0, TEXT("open"), szCmdLine, 0, 0, SW_SHOW);
		}
		
		return TRUE;
	}





