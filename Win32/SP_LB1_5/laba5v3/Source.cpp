/***
***									Лабараторная работа 2.1
***/


#include <Windows.h>
#include <tchar.h>
#include "resource.h"
#include <WindowsX.h>


LRESULT CALLBACK Pr2_WndProc(HWND, UINT, WPARAM, LPARAM);
BOOL CALLBACK Pr5_Dlg_WndProc(HWND, UINT, WPARAM, LPARAM);
void OnDestroy(HWND);
int OnCreate(HWND, LPCREATESTRUCT);
void OnPaint(HWND);
void OnCommand(HWND, int, HWND, UINT);

#define g_lpszClassName TEXT("sp_pr2_class")
#define g_lpszAplicationTitle TEXT("Главное окно приложения. Программист Латыш Д.Н.")
#define g_lpszDestroyMessage TEXT("Разрушается окно. В связи с этим поступило сообщение WM_DESTROY,из обработчика которого и выполнен данный вывод")
#define ID_MYBUTTON 1000
#define MAX_BYTES  10000

HINSTANCE hInstance;


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
	wc.hIcon = LoadIcon(hInstance, MAKEINTRESOURCE(IDI_ICON1));
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
		800,
		300,
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
		HANDLE_MSG(hWnd, WM_CREATE, OnCreate);
		HANDLE_MSG(hWnd, WM_DESTROY, OnDestroy);
		HANDLE_MSG(hWnd, WM_PAINT, OnPaint);
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

BOOL OnCreate(HWND hWnd, LPCREATESTRUCT OnCreateStruct)
{
	/*DrawMenuBar(hWnd);
	HWND hWnd_button = CreateWindow(TEXT("button"),
		TEXT("My button"),
		WS_CHILD | BS_PUSHBUTTON | WS_VISIBLE,
		20, 20, 100, 20,
		hWnd,
		(HMENU)ID_MYBUTTON,
		hInstance,
		NULL);*/
	return true;
}


void OnPaint(HWND hWnd)
{
	/*RECT rt;
	TCHAR lpszBUFF[200];
	PAINTSTRUCT ps;
	HDC hDC = BeginPaint(hWnd, &ps);
	EndPaint(hWnd, &ps);*/

	PAINTSTRUCT ps;//структура для прорисовки окна
	HDC hDC = BeginPaint(hWnd, &ps);//инициализируем контекст устройства
	HWND hStatic1 = CreateWindow(TEXT("static"),
		TEXT("Для открытия диалогового окна согласно задания 5 выберите Меню-Файл-Открыть"),
		WS_CHILD | WS_VISIBLE | SS_CENTER,
		100, 10, 200, 50,
		hWnd,
		(HMENU)0,
		hInstance,
		NULL);
	EndPaint(hWnd, &ps);//завершение обновления окна
}

void OnCommand(HWND hWnd, int id, HWND hwndCtl, UINT codeNotify)
{
	HMENU hMenu = GetMenu(hWnd);
	HMENU hMenuCorrect = GetSubMenu(hMenu, 1);

	switch (id)
	{
		case ID_FILE_OPEN:
		{
			//MessageBox(hWnd, TEXT("Выбрана команда Открыть"), TEXT("Menu Файл"), MB_OK);
			DialogBoxParam(hInstance, MAKEINTRESOURCE(IDD_DIALOG1), hWnd, Pr5_Dlg_WndProc, 0);
			break;
		}
		case ID_FILE_EXITALT:
		{
			MessageBox(hWnd, TEXT("Выбрана команда Выход"), TEXT("Menu Файл"), MB_OK);
			break;
		}
		case ID_VIEW_TEXT:
		{
			MessageBox(hWnd, TEXT("Выбрана команда Текст"), TEXT("Menu Просмотр"), MB_OK);
			break;
		}
		case ID_VIEW_GRAPHICS:
		{
			MessageBox(hWnd, TEXT("Выбрана команда Графика"), TEXT("Menu Просмотр"), MB_OK);
			break;
		}
		case ID_HELP_HELPCONTENT:
		{
			MessageBox(hWnd, TEXT("Выбрана команда Содержание справки"), TEXT("Menu Файл"), MB_OK);
			break;
		}
		case ID_HELP_ABOUT:
		{

			//DialogBoxParam(hInstance, MAKEINTRESOURCE(IDD_DIALOG1), hWnd, Pr5_Dlg_WndProc, 0);
			MessageBox(hWnd, TEXT("Выбрана команда О программе"), TEXT("Menu Файл"), MB_OK);
			break;
		}
		default:
		{
			/*TCHAR lpszBUFF1[100];
			ZeroMemory(&lpszBUFF1, sizeof(&lpszBUFF1));
													  
			wsprintf(lpszBUFF1, TEXT("Обработчик для команды с идентификатором %d отсутствует"), id);
			
			MessageBox(hWnd, lpszBUFF1, TEXT("Menu default"), MB_OK);*/
			break;
		}
	}
}

BOOL CALLBACK Pr5_Dlg_WndProc(HWND hDlg, UINT message, WPARAM wParam, LPARAM lParam)
{
	HWND hWnd_edit0, hWnd_listbox, hStatic;
	switch (message)
	{
	case WM_INITDIALOG:
	{
		hStatic = CreateWindow(TEXT("Static"),
			TEXT("Введите строку, скачайте в буфер, затем в листбокс. Для сброса нажать Сброс"),
			WS_CHILD | WS_VISIBLE | SS_CENTER,
			100, 10, 200, 50,
			hDlg,
			(HMENU)0,
			hInstance,
			NULL);
			hWnd_edit0 = CreateWindow(TEXT("edit"),
			NULL,
			WS_CHILD | WS_VISIBLE | WS_BORDER | ES_LEFT,
			100, 60, 200, 20,
			hDlg,
			(HMENU)IDC_EDIT1,
			hInstance,
			NULL);
			hWnd_listbox = CreateWindow(TEXT("listbox"),
			NULL,
			WS_CHILD | WS_VISIBLE | LBS_STANDARD | LBS_WANTKEYBOARDINPUT,
			100, 100, 200, 150,
			hDlg,
			(HMENU)IDC_LISTBOX,
			hInstance,
			NULL);
	}
	return TRUE;

	case WM_COMMAND:
	{
		static TCHAR BUFF[250];
		switch (LOWORD(wParam))
		{
		case IDC_BUTTON1:
		{
			SendMessage(GetDlgItem(hDlg, 1004), LB_ADDSTRING, 0, (LPARAM)(BUFF));
			memset(BUFF, 0, sizeof(&BUFF));
			break;
		}

		case IDC_BUTTON2:
		{
			memset(BUFF, 0, sizeof(&BUFF));
			GetDlgItemText(hDlg, 1003, BUFF, sizeof(BUFF));
			SetWindowText(GetDlgItem(hDlg, 1003), TEXT("\0"));
			break;
		}

		case IDC_BUTTON3:
		{
			SendMessage(GetDlgItem(hDlg, 1004), LB_RESETCONTENT, 0, 0);
			break;
		}
		case IDC_OK:
		{ EndDialog(hDlg, 0);
		return TRUE; }
		}
	}
	break;
	}
	return FALSE;
}