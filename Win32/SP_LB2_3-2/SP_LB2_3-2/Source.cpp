// SP_LB_2_3_Part2.cpp : Defines the entry point for the console application.
//

#include "stdafx.h"


int _tmain(int argc, _TCHAR* argv[])
{
	const int nTimerUnitsPerSecond = 10000000; //  столько 100-наносекундных интервалов в 1 секунде
	int numberCall = 0;
	LARGE_INTEGER li1, li2;

	TCHAR Buff1[500] = { 0 };
	HWND hMain = 0;
	TCHAR CmdParam[] = TEXT("notepad");
	HANDLE hTimer1 = CreateWaitableTimer(NULL, FALSE, NULL);
	HANDLE hTimer2 = CreateWaitableTimer(NULL, FALSE, NULL);
	HANDLE hNotepad;
	HANDLE hFile;
	STARTUPINFO si = { sizeof(si) };
	PROCESS_INFORMATION pi;

	printf("Console application with WaitableTimer\n");
	li1.QuadPart = -(15 * nTimerUnitsPerSecond);    // задаем значение с минусом тем самым сообщая,
	li2.QuadPart = -(10 * nTimerUnitsPerSecond);   // что нам надо относительное время а не абсолютное
	if (SetWaitableTimer(hTimer1, &li1, 15 * 1000, NULL, NULL, FALSE))
	{
		while (TRUE)
		{
			WaitForSingleObject(hTimer1, INFINITE);
			numberCall++;
			if (CreateProcess(NULL, CmdParam, NULL, NULL, FALSE, 0, NULL, NULL, &si, &pi))
			{
				SetWaitableTimer(hTimer2, &li2, 10 * 1000, NULL, NULL, FALSE);
				hNotepad = pi.hProcess;
			}
			else
				MessageBox(NULL, TEXT("Процесс не создан"), TEXT(""), MB_OK);
			wsprintf(Buff1, TEXT("Вызов номер %d"), numberCall);
			HWND hEdit1 = FindWindow(TEXT("Notepad"), TEXT("Безымянный - Блокнот"));
			HWND hEdit2 = GetWindow(hEdit1, GW_CHILD);
			SendMessage(hEdit2, WM_SETTEXT, 0, (LPARAM)Buff1);
			WaitForSingleObject(hTimer2, INFINITE);
			TerminateProcess(hNotepad, 12);
			continue;
		}
	}
	return 0;
}



