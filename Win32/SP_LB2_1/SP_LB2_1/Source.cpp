/***
***									Лабараторная работа 2 - 1
***/


#include <Windows.h>
#include <tchar.h>
#include "resource.h"
#include <WindowsX.h>
#define MAX_BYTES  10000

//-- Global variables ----
HANDLE ProcHandle[4];
DWORD ProcId[4];
HANDLE ThreadHandle[4];
DWORD ThreadId[4];
LPTSTR ProcImage[4];
TCHAR CmdParam[4][260];
HINSTANCE g_Inst;
static HWND hEdit;

HWND g_MyhWnd;
HANDLE ProcHandleSam = NULL;
DWORD ProcIdSam = NULL;


//-- Prototypes ----



LRESULT CALLBACK Pr2_WndProc(HWND, UINT, WPARAM, LPARAM);
void OnCommand(HWND, int, HWND, UINT);
void OnDestroy(HWND);
BOOL WndProc_OnCreate(HWND, LPCREATESTRUCT);



BOOL CALLBACK DlgProc(HWND, UINT, WPARAM, LPARAM);
BOOL DlgProc_OnInitDialog(HWND, HWND, LPARAM);
BOOL DlgProc_OnCommand(HWND, int, HWND, UINT);

BOOL CALLBACK TestProc(HWND, UINT, WPARAM, LPARAM);
BOOL TestProc_OnInitDialog(HWND, HWND, LPARAM);
BOOL TestProc_OnCommand(HWND, int, HWND, UINT);

_int64 FileTimeToQuadWord(PFILETIME);
PFILETIME QuadWordToFileTime(_int64, PFILETIME);

BOOL CALLBACK EnumWindowsProc(HWND, LPARAM);

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
		HANDLE_MSG(hWnd, WM_CREATE, WndProc_OnCreate);
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


BOOL WndProc_OnCreate(HWND hWnd, LPCREATESTRUCT lpCreateStruct)
{
	ProcImage[0] = TEXT("SP_PR2-1.exe");
	ProcImage[1] = TEXT("notepad.exe");
	ProcImage[2] = TEXT("notepad.exe");
	ProcImage[3] = TEXT("calc.exe");

	TCHAR CmdParam0[] = TEXT("D:\\POIS\\ОАПЯВУ\\C#\\Labs\\Win32\\SP_LB2_1\\Debug\\SP_LB_1.exe");
	TCHAR CmdParam1[] = TEXT("notepad");
	TCHAR CmdParam2[] = TEXT("notepad D:\\POIS\\ОАПЯВУ\\C#\\Labs\\Win32\\SP_LB2_1\\SP_LB2_1\\source.cpp");
	TCHAR CmdParam3[] = TEXT("calc");
	_tcscpy_s(CmdParam[0], CmdParam0);
	_tcscpy_s(CmdParam[1], CmdParam1);
	_tcscpy_s(CmdParam[2], CmdParam2);
	_tcscpy_s(CmdParam[3], CmdParam3);

	DuplicateHandle
	(
		GetCurrentProcess(), // описатель процесса, к которому относится псевдоописатель;
		GetCurrentProcess(), // псевдоописатель процесса;
		GetCurrentProcess(), // описатель процесса, к которому относится новый, настоящий описатель;
		&ProcHandle[0], // даст новый, настоящий описатель, идентифицирующий процесс;
		0, // игнорируется из-за DUPLICATE_SAME_ACCESS;
		FALSE, // новый описатель процесса ненаследуемый;
		DUPLICATE_SAME_ACCESS
	); // новому описателю процесса присваиваются те же атрибуты защиты, что и псевдоописателю

	ProcId[0] = GetCurrentProcessId();

	DuplicateHandle
	(
		GetCurrentProcess(), // описатель процесса.к которому относится псевдоописатель потока;
		GetCurrentThread(), // псевдоописатель родительского потока;
		GetCurrentProcess(), // описатель процесса, к которому относится новый, настоящий описатель потока;
		&ThreadHandle[0], // даст новый, настоящий описатель, идентифицирующий родительский поток;
		0, // игнорируется из-за DUPLICATE_SAME_ACCESS;
		FALSE, // новый описатель потока ненаследуемый;
		DUPLICATE_SAME_ACCESS
	); // новому описателю потока присваиваются те же атрибуты защиты, что и псевдоописателю
	ThreadId[0] = GetCurrentThreadId();

	hEdit = CreateWindowEx(0L, TEXT("Edit"), TEXT(""),
		WS_CHILD | WS_BORDER | WS_VISIBLE | ES_MULTILINE,
		10, 10, 565, 420, hWnd, (HMENU)ID_PROCESS_INDEPENDETWORK,
		g_Inst, NULL);
		
	return -1;
}


void OnCommand(HWND hWnd, int id, HWND hwndCtl, UINT codeNotify)
{
	STARTUPINFO si = { sizeof(si) };
	PROCESS_INFORMATION pi;
	DWORD dwExitCode = 9;

	switch (id)
	{
	case ID_PROCESS_NOTEPAD:///////////////////////////////////////////////////////
	{
		// создаём объект ядра со счётчиком пользователей 1
		if (CreateProcess(NULL,   // module name
			CmdParam[1],			// Command line
			NULL,           // Process handle not inheritable
			NULL,           // Thread handle not inheritable
			FALSE,          // Set handle inheritance to FALSE
			0,              // No creation flags
			NULL,           // Use parent's environment block
			NULL,           // Use parent's starting directory 
			&si,            // Pointer to STARTUPINFO structure
			&pi))           // Pointer to PROCESS_INFORMATION structure
		{
			ProcHandle[1] = pi.hProcess;
			ProcId[1] = pi.dwProcessId;
			ThreadHandle[1] = pi.hThread;
			ThreadId[1] = pi.dwThreadId;

			// закрываем дескрипторы созданных процесса и потока
			CloseHandle(ProcHandle[1]);
			CloseHandle(ThreadHandle[1]);
		}
		else
			MessageBox(NULL, TEXT("Процесс не создан"), TEXT(""), MB_OK);
	}
	break;

	//case ID_PROCESS_NOTEPADWITHTEXT://////////// для основной работы
	//{
	//	// создаём объект ядра со счётчиком пользователей 1
	//	if (CreateProcess(NULL, CmdParam[2], NULL, NULL, FALSE, 0, NULL, NULL, &si, &pi))
	//	{
	//		ProcHandle[2] = pi.hProcess;
	//		ProcId[2] = pi.dwProcessId;
	//		ThreadHandle[2] = pi.hThread;
	//		ThreadId[2] = pi.dwThreadId;
	//	}
	//	else
	//		MessageBox(NULL, TEXT("Процесс не создан"), TEXT(""), MB_OK);
	//}
	//break;



	case ID_PROCESS_CALCULATOR://////////////////////////////////////////////////////////
	{
		// создаём объект ядра со счётчиком пользователей 1
		if (CreateProcess(NULL, CmdParam[3], NULL, NULL, FALSE, 0, NULL, NULL, &si, &pi))
		{
			ProcHandle[3] = pi.hProcess;
			ProcId[3] = pi.dwProcessId;
			ThreadHandle[3] = pi.hThread;
			ThreadId[3] = pi.dwThreadId;

		}
		else
			MessageBox(NULL, TEXT("Процесс не создан"), TEXT(""), MB_OK);
	}
	break;

	case ID_PROCESS_CLOSECALC://///////////////////////////
	{
		if (ProcHandle[3] != NULL)
		{
			TCHAR result[300];

			TerminateProcess(ProcHandle[3], 9);
			GetExitCodeProcess(ProcHandle[3], &dwExitCode);
			if (dwExitCode == STILL_ACTIVE)
				wsprintf(result, TEXT("Процесс ещё не навершился"));
			else
				wsprintf(result, TEXT("код (вариант в группе) %d"), dwExitCode);
			MessageBox(hWnd, result, TEXT("Код завершения"), MB_OK);
		}
		else
		{
			MessageBox(hWnd, TEXT("Процесс небыл создан"), TEXT("Ошибка"), MB_OK);
		}
	}
	break;


	case ID_ABOUTPROCESS_CURRENTPROC:
		DialogBoxParam(g_Inst, MAKEINTRESOURCE(IDD_DIALOG1), hWnd, (DLGPROC)DlgProc, 0); // последний параметр "за кулисами" передается как последний параметр с инфой для функции типа DLGPROC
		break;

	case ID_ABOUTPROCESS_NOTEPAD:
		if (ProcHandle[1] != NULL)
		{
			DialogBoxParam(g_Inst, MAKEINTRESOURCE(IDD_DIALOG1),
				hWnd,
				(DLGPROC)DlgProc,
				1);
		}
		else
			MessageBox(hWnd, TEXT("Процесс не создан"), TEXT("Ошибка"), MB_OK);
		break;

	case ID_ABOUTPROCESS_NOTEPADWITHTEXT:
		if (ProcHandle[2] != NULL)
		{
			DialogBoxParam(g_Inst, MAKEINTRESOURCE(IDD_DIALOG1),
				hWnd,
				(DLGPROC)DlgProc,
				2);
		}
		else
			MessageBox(hWnd, TEXT("Процесс не создан"), TEXT("Ошибка"), MB_OK);
		break;

	case ID_ABOUTPROCESS_CALCULATOR:
		if (ProcHandle[3] != NULL)
		{
			DialogBoxParam(g_Inst, MAKEINTRESOURCE(IDD_DIALOG1),
				hWnd,
				(DLGPROC)DlgProc,
				3);
		}
		else
			MessageBox(hWnd, TEXT("Процесс не создан"), TEXT("Ошибка"), MB_OK);
		break;

		case ID_PROCESS_NOTEPADWITHTEXT://///////// для самостоятельной работы
		{
			OPENFILENAME ofn;   // структура для common dialog box
			TCHAR lpszFileSpec[260];   // массив для имени файла

			//Иницализация OPENFILENAME
			ZeroMemory(&ofn, sizeof(OPENFILENAME));
			ofn.lStructSize = sizeof(OPENFILENAME);
			ofn.hwndOwner = hWnd;  // hwnd – дескриптор окна–влвдельца
			ofn.lpstrFile = lpszFileSpec;
			ofn.lpstrFile[0] = '\0';
			ofn.nMaxFile = sizeof(lpszFileSpec);
			// Формирование массива строк шаблонов фильтра
			ofn.lpstrFilter = TEXT("All\0*.*\0Text\0*.TXT\0");
			ofn.nFilterIndex = 1; // Индекс для текущего шаблона фильтра
			ofn.lpstrFileTitle = NULL; // Без заголовка
			ofn.nMaxFileTitle = 0;
			ofn.lpstrInitialDir = NULL; // В качестве начального текущий каталог
			ofn.Flags = OFN_PATHMUSTEXIST | OFN_FILEMUSTEXIST;

			// Отображение диалогового окна
			BOOL fRet = GetOpenFileName(&ofn);

			if (fRet != FALSE)
			{
				TCHAR Buff1[100] = { 0 };
				TCHAR temp[100] = TEXT("notepad ");
				_tcscpy_s(Buff1, ofn.lpstrFile);
				int j = 8;
				for (int i = 0; i < _tcslen(Buff1); i++)
				{
					temp[j] = Buff1[i];
					if (Buff1[i] == '\\')
					{
						temp[j + 1] = '\\';
						j++;
					}
					j++;
				}
				_tcscpy_s(CmdParam[2], temp);

				if (CreateProcess(NULL, CmdParam[2], NULL, NULL, FALSE, 0, NULL, NULL, &si, &pi))
				{
					ProcHandle[2] = pi.hProcess;
					ProcId[2] = pi.dwProcessId;
					ThreadHandle[2] = pi.hThread;
					ThreadId[2] = pi.dwThreadId;
				}
				else
					MessageBox(NULL, TEXT("Процесс не создан"), TEXT(""), MB_OK);
			}
		}
		break;

		////////////// Для самостоятельной работы /////////////////////
	case IDM_PROC_TESTPROC:

		DialogBoxParam(g_Inst, MAKEINTRESOURCE(IDD_DIALOG2),
			hWnd,
			(DLGPROC)TestProc,
			NULL);

		break;

	case ID_PROCESS_INDEPENDETWORK:
	{
		DWORD dwNumbOfBytes = MAX_BYTES;
		TCHAR Buffer[MAX_BYTES] = { 0 };
		HANDLE hFile = NULL;
		TCHAR CmdParam7[260];
		OPENFILENAME ofn;   // структура для common dialog box
		TCHAR lpszFileSpec[260];   // массив для имени файла

								   //Иницализация OPENFILENAME
		ZeroMemory(&ofn, sizeof(OPENFILENAME));
		ofn.lStructSize = sizeof(OPENFILENAME);
		ofn.hwndOwner = hWnd;  // hwnd – дескриптор окна–влвдельца
		ofn.lpstrFile = lpszFileSpec;
		ofn.lpstrFile[0] = '\0';
		ofn.nMaxFile = sizeof(lpszFileSpec);
		// Формирование массива строк шаблонов фильтра
		ofn.lpstrFilter = TEXT("All\0*.*\0Text\0*.TXT\0");
		ofn.nFilterIndex = 1; // Индекс для текущего шаблона фильтра
		ofn.lpstrFileTitle = NULL; // Без заголовка
		ofn.nMaxFileTitle = 0;
		ofn.lpstrInitialDir = NULL; // В качестве начального текущий каталог
		ofn.Flags = OFN_PATHMUSTEXIST | OFN_FILEMUSTEXIST;

		// Отображение диалогового окна
		BOOL fRet = GetOpenFileName(&ofn);

		if (fRet != FALSE)
		{
			TCHAR Buff1[100] = { 0 };
			TCHAR temp[100] = TEXT("notepad ");
			_tcscpy_s(Buff1, ofn.lpstrFile);
			int j = 8;
			for (int i = 0; i < _tcslen(Buff1); i++)
			{
				temp[j] = Buff1[i];
				if (Buff1[i] == '\\')
				{
					temp[j + 1] = '\\';
					j++;
				}
				j++;
			}
			_tcscpy_s(CmdParam7, temp);

			if (!CreateProcess(NULL, CmdParam7, NULL, NULL, FALSE, 0, NULL, NULL, &si, &pi))
				MessageBox(NULL, TEXT("Процесс не создан"), TEXT(""), MB_OK);


			WaitForSingleObject(pi.hProcess, INFINITE);


			hFile = CreateFile(ofn.lpstrFile, GENERIC_READ,
				0, (LPSECURITY_ATTRIBUTES)NULL,
				OPEN_EXISTING, FILE_ATTRIBUTE_NORMAL,
				(HANDLE)NULL);
			//Проверка корректности открытия файла
			if (hFile != INVALID_HANDLE_VALUE)
			{
				ReadFile(hFile, Buffer, dwNumbOfBytes, &dwNumbOfBytes, NULL);
				SetDlgItemText(hWnd, ID_PROCESS_INDEPENDETWORK, Buffer);
			}
		}
		CloseHandle(hFile);
		CloseHandle(pi.hProcess);
		CloseHandle(pi.hThread);
	}

	break;

	default:
		break;
	}



}

/////////////////////////////////////////////////////////////////////////////////
BOOL CALLBACK DlgProc(HWND hdlg, UINT msg, WPARAM wParam, LPARAM lParam)
{
	switch (msg)
	{
		HANDLE_MSG(hdlg, WM_INITDIALOG, DlgProc_OnInitDialog);
		HANDLE_MSG(hdlg, WM_COMMAND, DlgProc_OnCommand);
	}	return FALSE;
}


BOOL DlgProc_OnCommand(HWND hdlg, int id, HWND hwndCtl, UINT codeNotify)
{
	switch (id)
	{
	case IDOK:
		EndDialog(hdlg, IDOK);
		return TRUE;
	}
}

BOOL DlgProc_OnInitDialog(HWND hdlg, HWND hwndFocus, LPARAM lParam)
{
	HWND hEdit1 = GetDlgItem(hdlg, IDC_EDIT1);
	HWND hEdit2 = GetDlgItem(hdlg, IDC_EDIT2);
	HWND hEdit3 = GetDlgItem(hdlg, IDC_EDIT3);
	HWND hEdit4 = GetDlgItem(hdlg, IDC_EDIT4);
	HWND hEdit5 = GetDlgItem(hdlg, IDC_EDIT5);
	HWND hEdit6 = GetDlgItem(hdlg, IDC_EDIT6);
	HWND hEdit7 = GetDlgItem(hdlg, IDC_EDIT7);
	HWND hEdit8 = GetDlgItem(hdlg, IDC_EDIT8);
	HWND hEdit9 = GetDlgItem(hdlg, IDC_EDIT9);
	HWND hEdit10 = GetDlgItem(hdlg, IDC_EDIT10);
	HWND hEdit11 = GetDlgItem(hdlg, IDC_EDIT11);
	HWND hEdit12 = GetDlgItem(hdlg, IDC_EDIT12);
	HWND hEdit13 = GetDlgItem(hdlg, IDC_EDIT13);


	TCHAR Buff1[256] = { 0 };
	DWORD exitCodeProc = NULL;
	DWORD exitCodeThread = NULL;
	DWORD prior = NULL;


	lstrcpy(Buff1, ProcImage[lParam]);
	SendMessage(hEdit1, WM_SETTEXT, 0, (LPARAM)Buff1);

	lstrcpy(Buff1, CmdParam[lParam]);
	SendMessage(hEdit6, WM_SETTEXT, 0, (LPARAM)Buff1);

	wsprintf(Buff1, TEXT("%d"), ProcHandle[lParam]);
	SendMessage(hEdit2, WM_SETTEXT, 0, (LPARAM)Buff1);

	wsprintf(Buff1, TEXT("%d"), ProcId[lParam]);
	SendMessage(hEdit3, WM_SETTEXT, 0, (LPARAM)Buff1);

	wsprintf(Buff1, TEXT("%d"), ThreadHandle[lParam]);
	SendMessage(hEdit4, WM_SETTEXT, 0, (LPARAM)Buff1);

	wsprintf(Buff1, TEXT("%d"), ThreadId[lParam]);
	SendMessage(hEdit5, WM_SETTEXT, 0, (LPARAM)Buff1);

	GetExitCodeProcess(ProcHandle[lParam], &exitCodeProc);
	if (exitCodeProc == STILL_ACTIVE)
		wsprintf(Buff1, TEXT("Состояние - Активен"));
	else
		wsprintf(Buff1, TEXT("%d"), exitCodeProc);
	SendMessage(hEdit7, WM_SETTEXT, 0, (LPARAM)Buff1);

	GetExitCodeThread(ThreadHandle[lParam], &exitCodeThread);
	if (exitCodeThread == STILL_ACTIVE)
		wsprintf(Buff1, TEXT("Состояние - Активен"));
	else
		wsprintf(Buff1, TEXT("%d"), exitCodeThread);
	SendMessage(hEdit8, WM_SETTEXT, 0, (LPARAM)Buff1);


	prior = GetPriorityClass(ProcHandle[lParam]);

	switch (prior)
	{
	case REALTIME_PRIORITY_CLASS:
		lstrcpy(Buff1, TEXT("Real-time"));
		break;

	case HIGH_PRIORITY_CLASS:
		lstrcpy(Buff1, TEXT("High"));
		break;

	case ABOVE_NORMAL_PRIORITY_CLASS:
		lstrcpy(Buff1, TEXT("Above normal"));
		break;

	case NORMAL_PRIORITY_CLASS:
		lstrcpy(Buff1, TEXT("Normal"));
		break;

	case BELOW_NORMAL_PRIORITY_CLASS:
		lstrcpy(Buff1, TEXT("Below normal"));
		break;

	case IDLE_PRIORITY_CLASS:
		lstrcpy(Buff1, TEXT("Idle"));
		break;
	}
	SendMessage(hEdit9, WM_SETTEXT, 0, (LPARAM)Buff1);


	FILETIME ftCreation, ftExit, ftKernel, ftUser, ftWorking, ftWaiting;
	_int64 working64, temp64, waiting64;
	SYSTEMTIME stCreation, stExit, stKernel, stUser, stWorkind, stWaiting;
	if (GetProcessTimes(ProcHandle[lParam], &ftCreation, &ftExit, &ftKernel, &ftUser))
	{
		FileTimeToSystemTime(&ftUser, &stUser);
		FileTimeToSystemTime(&ftKernel, &stKernel);

		if (exitCodeProc == STILL_ACTIVE)
		{
			SYSTEMTIME stCurrentTime;
			FILETIME ftCurrentTime;
			GetSystemTime(&stCurrentTime);

			SystemTimeToFileTime(&stCurrentTime, &ftCurrentTime);
			working64 = FileTimeToQuadWord(&ftCurrentTime) - FileTimeToQuadWord(&ftCreation);
		}
		else
			working64 = FileTimeToQuadWord(&ftExit) - FileTimeToQuadWord(&ftCreation);

		QuadWordToFileTime(working64, &ftWorking);
		FileTimeToSystemTime(&ftWorking, &stWorkind);
		wsprintf(Buff1, TEXT("%d мин, %d с, %d мс"), stWorkind.wMinute, stWorkind.wSecond, stWorkind.wMilliseconds);//время жизни процесса
		SendMessage(hEdit10, WM_SETTEXT, 0, (LPARAM)Buff1);

		wsprintf(Buff1, TEXT("%d мин, %d с, %d мс"), stUser.wMinute, stUser.wSecond, stUser.wMilliseconds);//в пользовательском режиме
		SendMessage(hEdit11, WM_SETTEXT, 0, (LPARAM)Buff1);
		wsprintf(Buff1, TEXT("%d мин, %d с, %d мс"), stKernel.wMinute, stKernel.wSecond, stKernel.wMilliseconds);//в режиме ядра
		SendMessage(hEdit12, WM_SETTEXT, 0, (LPARAM)Buff1);

		temp64 = FileTimeToQuadWord(&ftKernel) + FileTimeToQuadWord(&ftUser);
		waiting64 = working64 - temp64;
		QuadWordToFileTime(waiting64, &ftWaiting);
		FileTimeToSystemTime(&ftWaiting, &stWaiting);

		wsprintf(Buff1, TEXT("%d мин, %d с, %d мс"), stWaiting.wMinute, stWaiting.wSecond, stWaiting.wMilliseconds);//время простоя
		SendMessage(hEdit13, WM_SETTEXT, 0, (LPARAM)Buff1);
	}

	return TRUE;
}


_int64 FileTimeToQuadWord(PFILETIME pFileTime)
{
	_int64 qw;
	qw = pFileTime->dwHighDateTime;
	qw <<= 32;
	qw |= pFileTime->dwLowDateTime;
	return qw;
}


PFILETIME QuadWordToFileTime(_int64 qw, PFILETIME pFileTime)
{
	pFileTime->dwHighDateTime = (DWORD)(qw >> 32);
	pFileTime->dwLowDateTime = (DWORD)(qw & 0xffffffff);
	return pFileTime;
}

/////// для самостоятельной работы /////////////////

BOOL CALLBACK TestProc(HWND hdlg, UINT msg, WPARAM wParam, LPARAM lParam)
{
	switch (msg)
	{
		HANDLE_MSG(hdlg, WM_INITDIALOG, TestProc_OnInitDialog);
		HANDLE_MSG(hdlg, WM_COMMAND, TestProc_OnCommand);
	}	return FALSE;
}

BOOL TestProc_OnInitDialog(HWND hdlg, HWND hwndFocus, LPARAM lParam)
{
	HWND hR = GetDlgItem(hdlg, IDC_RADIO1);
	SendMessage(hR, BM_SETCHECK, 1, 0);

	SetDlgItemText(hdlg, IDC_EDIT_WIDTH, TEXT("400"));
	SetDlgItemText(hdlg, IDC_EDIT_HEIGHT, TEXT("250"));
	SetDlgItemText(hdlg, IDC_EDIT_XPOS, TEXT("100"));
	SetDlgItemText(hdlg, IDC_EDIT_YPOS, TEXT("30"));

	return TRUE;
}

BOOL TestProc_OnCommand(HWND hdlg, int id, HWND hwndCtl, UINT codeNotify)
{
	STARTUPINFO si;
	PROCESS_INFORMATION pi;
	ZeroMemory(&pi, sizeof(pi));
	ZeroMemory(&si, sizeof(si));
	DWORD dwExitCode = 0;
	DWORD wShowWind;
	TCHAR buffer1[100];

	switch (id)
	{
	case IDOK:
	{
		EndDialog(hdlg, IDOK);


		HWND hWidth = GetDlgItem(hdlg, IDC_EDIT_WIDTH);
		HWND hHeight = GetDlgItem(hdlg, IDC_EDIT_HEIGHT);
		HWND hXpos = GetDlgItem(hdlg, IDC_EDIT_XPOS);
		HWND hYpos = GetDlgItem(hdlg, IDC_EDIT_YPOS);

		GetWindowText(hWidth, (LPWSTR)buffer1, sizeof(buffer1));
		si.dwXSize = _ttoi(buffer1);
		GetWindowText(hHeight, (LPWSTR)buffer1, sizeof(buffer1));
		si.dwYSize = _ttoi(buffer1);
		GetWindowText(hXpos, (LPWSTR)buffer1, sizeof(buffer1));
		si.dwX = _ttoi(buffer1);
		GetWindowText(hYpos, (LPWSTR)buffer1, sizeof(buffer1));
		si.dwY = _ttoi(buffer1);

		GetExitCodeProcess(ProcHandleSam, &dwExitCode);

		if (dwExitCode == STILL_ACTIVE)
		{
			EnumWindows((WNDENUMPROC)EnumWindowsProc, (LPARAM)ProcIdSam);

			SetWindowPos(g_MyhWnd, 0, si.dwX, si.dwY, si.dwXSize, si.dwYSize, NULL);

			if (IsDlgButtonChecked(hdlg, IDC_RADIO1))
				SendMessage(g_MyhWnd, WM_SYSCOMMAND, SC_RESTORE, 0);
			if (IsDlgButtonChecked(hdlg, IDC_RADIO2))
				SendMessage(g_MyhWnd, WM_SYSCOMMAND, SC_MINIMIZE, 0);
			if (IsDlgButtonChecked(hdlg, IDC_RADIO3))
				SendMessage(g_MyhWnd, WM_SYSCOMMAND, SC_MAXIMIZE, 0);
		}
		else
		{
			TCHAR CmdTest[] = TEXT("D:\\POIS\\ОАПЯВУ\\C#\\Labs\\Win32\\SP_LB2_1\\Debug\\SP_LB_1.exe");

			si.cb = sizeof(STARTUPINFO);

			si.lpTitle = TEXT("TestProc");

			if (IsDlgButtonChecked(hdlg, IDC_RADIO1))
				si.wShowWindow = SW_NORMAL;
			if (IsDlgButtonChecked(hdlg, IDC_RADIO2))
				si.wShowWindow = SW_MINIMIZE;
			if (IsDlgButtonChecked(hdlg, IDC_RADIO3))
				si.wShowWindow = SW_MAXIMIZE;

			si.dwFlags = STARTF_USESIZE | STARTF_USEPOSITION | STARTF_USESHOWWINDOW;

			if (!CreateProcess(NULL, CmdTest, NULL, NULL, FALSE, 0, NULL, NULL, &si, &pi))
				MessageBox(NULL, TEXT("Процесс не создан"), TEXT(""), MB_OK);
			ProcHandleSam = pi.hProcess;
			ProcIdSam = pi.dwProcessId;
		}

	}
	return TRUE;
	case IDCANCEL:
		EndDialog(hdlg, IDCANCEL);
		return TRUE;
	}
}

BOOL CALLBACK EnumWindowsProc(HWND hwnd, LPARAM lParam)
{
	DWORD dwPID;
	GetWindowThreadProcessId(hwnd, &dwPID);

	if (dwPID == (DWORD)lParam)
	{
		g_MyhWnd = hwnd;

		return FALSE;
	}
	return TRUE;
}






