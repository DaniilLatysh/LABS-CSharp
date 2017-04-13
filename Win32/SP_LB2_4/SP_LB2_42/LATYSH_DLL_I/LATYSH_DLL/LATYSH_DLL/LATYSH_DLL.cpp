// LATYSH_DLL.cpp : Defines the exported functions for the DLL application.
//

#include "stdafx.h"
#include <stdio.h>
#include <stdlib.h>
#include <conio.h>
#include <process.h>   
#include <locale.h>     
#include <string.h>
#include <ctype.h>
#include <windows.h>
#include "LATYSH_DLL.h"



FILE* OpenFile(LPCTSTR path_file)
{
	FILE* file = fopen("C:\\Users\\Danila\\Desktop\\text.txt", "r");
	if (file == NULL)
	{
		MessageBox(NULL, L"Error open file", L"Error", MB_OK);
		return 0;
	}
	return file;
}


void SearchStringFile(FILE*file)
{
	int count = 0;
	char*tokenPtr;
	char buff[1000] = { 0 };
	char add_buff[200] = { 0 };

	if (buff == NULL || add_buff == NULL)
	{
		MessageBox(NULL, L"Memory overflow", L"Error", MB_OK);
		return;
	}
	do
	{
		fgets(buff, sizeof(buff), file);
		tokenPtr = strtok(buff, "!?,.:- ");
		while (tokenPtr != NULL)
		{
			if (strlen(tokenPtr) <= 4)
			{
				count++;
			}
			tokenPtr = strtok(NULL, "!?,.:- ");
		}
	} while (feof(file) == 0);
	if (count != 0)
	{
		sprintf(add_buff, "\n Word with the number of characthers <=4 %d", count);
		file = fopen("C:\\Users\\Danila\\Desktop\\text.txt", "a + ");
			fwrite(&add_buff, sizeof(char), strlen(add_buff), file);
		fclose(file);
	}
}




