/***
***									Лабараторная работа 2 - 1
***/


#include <Windows.h>
#include <tchar.h>
#include "resource.h"
#include <WindowsX.h>


HANDLE ProcHandle[4];
DWORD ProcId[4];
HANDLE ThreadHandle[4];
DWORD TherdId[4];
LPTSTR ProcImage[4];
TCHAR CmdParam[4][260];



LRESULT CALLBACK Pr2_WndProc(HWND, UINT, WPARAM, LPARAM);
void OnCommand(HWND, int, HWND, UINT);
void OnDestroy(HWND);

#define g_lpszClassName TEXT("sp_pr2_class")
#define g_lpszAplicationTitle TEXT("Главное окно приложения. Программист Латыш Д.Н.")


int APIENTRY _tWinMain(
	HINSTANCE hInstance,
	HINSTANCE hPrevInstance,
	LPTSTR lpszCmdLine,
	int nCmdShow)
{
	WNDCLASSEX wc;
	MSG msg;
	HWND hWnd;
	HMENU hMenu;

	memset(&wc, 0, sizeof(WNDCLASSEX));
	wc.cbSize = sizeof(WNDCLASSEX);
	wc.lpszClassName = g_lpszClassName;
	wc.lpfnWndProc = Pr2_WndProc;
	wc.style = CS_VREDRAW | CS_HREDRAW;
	wc.hInstance = hInstance;
	wc.hIcon = NULL;
	wc.hCursor = LoadCursor(hInstance, MAKEINTRESOURCE(IDC_ARROW));
	wc.hbrBackground = CreateSolidBrush(RGB(0, 127, 127));
	wc.lpszMenuName = NULL;
	wc.cbClsExtra = 0;
	wc.cbWndExtra = 0;

	if (!RegisterClassEx(&wc))
	{
		MessageBeep(MB_ICONASTERISK);
		MessageBox(NULL, TEXT("Ошибка регистрации класса окна"), TEXT("Ошибка"), MB_OK | MB_ICONERROR);
		return FALSE;
	}

	hMenu = LoadMenu(hInstance, MAKEINTRESOURCE(IDR_MENU1));

	hWnd = CreateWindowEx(NULL,
		g_lpszClassName,
		g_lpszAplicationTitle,
		WS_OVERLAPPEDWINDOW,
		200,
		200,
		600,
		500,
		NULL,
		hMenu,
		hInstance,
		NULL);

	if (!hWnd)
	{
		MessageBeep(MB_ICONASTERISK);
		MessageBox(NULL, TEXT("Окно не создано!"), TEXT("Ошибка"), MB_OK | MB_ICONERROR);
		return FALSE;
	}
	ShowWindow(hWnd, nCmdShow);
	UpdateWindow(hWnd);


	while (GetMessage(&msg, NULL, 0, 0))
	{
		TranslateMessage(&msg);
		DispatchMessage(&msg);
	}
	return msg.wParam;
}


LRESULT CALLBACK Pr2_WndProc(HWND hWnd, UINT msg, WPARAM wParam, LPARAM lParam)
{
	switch (msg)
	{
		HANDLE_MSG(hWnd, WM_DESTROY, OnDestroy);
		HANDLE_MSG(hWnd, WM_COMMAND, OnCommand);
	default:
		return(DefWindowProc(hWnd, msg, wParam, lParam));
		break;
	}
	return 5;
}

void OnDestroy(HWND)
{
	PostQuitMessage(0);
}


void OnCommand(HWND hWnd, int id, HWND hwndCtl, UINT codeNotify)
{
	HMENU hMenu = GetMenu(hWnd);
	HMENU hMenuCorrect = GetSubMenu(hMenu, 1);

	PAINTSTRUCT ps;
	HDC hdc;
	

	static	HWND hwndNotepad = 0;
	static	HWND hwndCalc = 0;
	BOOL fSucsess = FALSE;


	switch (id)
	{
	case ID_PROCESS_NOTEPAD:
	{

		PROCESS_INFORMATION pi;
		STARTUPINFO sti;


		ZeroMemory(&sti, sizeof(STARTUPINFO));
		sti.cb = sizeof(STARTUPINFO);

		hwndNotepad = FindWindow(TEXT("Notepad"), NULL);
		if (!hwndNotepad)
		{
			fSucsess = CreateProcess(NULL, TEXT("notepad.exe"),
				NULL, NULL, 0, 0, NULL, NULL, &sti, &pi);
			

			if (fSucsess)
			{
				while (!hwndNotepad) 
				{
					hwndNotepad = FindWindow(TEXT("Notepad"), NULL);
				}

				ProcHandle[1] = pi.hProcess;
				ProcId[1] = pi.dwProcessId;
				ThreadHandle[1] = pi.hThread;
				TherdId[1] = pi.dwThreadId;
				

				MessageBox(NULL, TEXT("Sucsess"),
					TEXT("Info"), MB_OK);
				WaitForSingleObject(pi.hProcess, INFINITE); // Wait for close

				CloseHandle(pi.hThread); // destroy handle
				CloseHandle(pi.hProcess); // destroy handle

			}
			else
				MessageBox(NULL, TEXT("Process was not create"),
					TEXT("Info"), MB_OK);
		}
		else
			MessageBox(NULL, TEXT("Process exist"),
				TEXT("Info"), MB_OK);

		break;
	}
	case ID_PROCESS_NOTEPADWITHTEXT:
	{
		
		break;
	}
	case ID_PROCESS_CALCULATOR:
	{
		PROCESS_INFORMATION pi;
		STARTUPINFO sti;


		ZeroMemory(&sti, sizeof(STARTUPINFO));
		sti.cb = sizeof(STARTUPINFO);

		hwndCalc = FindWindow(TEXT("Calculator"), NULL);
		if (!hwndCalc)
		{
			fSucsess = CreateProcess(NULL, TEXT("calc.exe"),
				NULL, NULL, 0, 0, NULL, NULL, &sti, &pi);


			if (fSucsess)
			{
				while (!hwndCalc)
				{
					hwndCalc = FindWindow(TEXT("Calculator"), NULL);
				}

				ProcHandle[2] = pi.hProcess;
				ProcId[2] = pi.dwProcessId;
				ThreadHandle[2] = pi.hThread;
				TherdId[2] = pi.dwThreadId;

				MessageBox(NULL, TEXT("Sucsess"),
					TEXT("Info"), MB_OK);

				WaitForSingleObject(pi.hProcess, INFINITE); // Wait for close

				CloseHandle(pi.hThread); // destroy handle
				CloseHandle(pi.hProcess); // destroy handle

			}
			else
				MessageBox(NULL, TEXT("Process was not create"),
					TEXT("Info"), MB_OK);
		}
		else
			MessageBox(NULL, TEXT("Process exist"),
				TEXT("Info"), MB_OK);

		break;
	}
	case ID_PROCESS_CLOSECALC:
	{
		
		break;
	}
	case ID_ABOUTPROCESS_CURRENTPROC:
	{
		
		break;
	}
	case ID_ABOUTPROCESS_NOTEPAD:
	{

		
		break;
	}
	case ID_ABOUTPROCESS_NOTEPADWITHTEXT:
	{


		break;
	}
	case ID_ABOUTPROCESS_CALCULATOR:
	{


		break;
	}
	default:
	{

		break;
	}
	}
}