#include <Windows.h>
#include <windowsx.h>
#include <tchar.h>
#include "resource.h"


#define g_lpszClassName TEXT("sp_pr2_class");
#define g_lpszAplicationTitle TEXT(" Главное окно приложения. Програмист Латыш Даниил");


HINSTANCE h_hInst;
HMENU g_hMenu;
MENUITEMINFO g_CloseFile;
HWND g_hListBox;
LRESULT CALLBACK Pr2_WndProc(HWND, UINT, WPARAM, LPARAM);
LRESULT CALLBACK ChildProc(HWND, UINT, WPARAM, LPARAM);
BOOL WndProc_OnCreate(HWND, LPCREATESTRUCT);
void WndProc_COMMAND(HWND, int, HWND, UINT);
void WndProc_MENUSELECT(HWND, HMENU, int, HMENU, UINT);
void WndProc_RBUTTONDOWN(HWND, BOOL, int, int, UINT);
void WndProc_OnDestroy(HWND);



int APIENTRY _tWinMain(HINSTANCE hInstance, HINSTANCE hPrevInstance, LPTSTR lpszCmdLine, int nCmdShow)
{
	LPCTSTR name = g_lpszClassName;
	LPCTSTR title = g_lpszAplicationTitle;
	
	HWND hWnd;
	WNDCLASSEX wc;
	MSG msg;
	

	h_hInst = hInstance;
	g_hMenu = LoadMenu(h_hInst, (LPCTSTR)IDR_MENU1);


	HBRUSH hbr = CreateSolidBrush(RGB(0, 127, 127));
	memset(&wc, 0, sizeof(WNDCLASSEX));
	wc.cbSize = sizeof(WNDCLASSEX);
	wc.lpszClassName = name;
	wc.lpfnWndProc = Pr2_WndProc;
	wc.style = CS_VREDRAW | CS_HREDRAW;
	wc.hInstance = hInstance;
	wc.hIcon = LoadIcon(h_hInst, MAKEINTRESOURCE(IDI_ICON1));
	wc.hCursor = LoadCursor(h_hInst, MAKEINTRESOURCE(IDC_CURSOR1));
	wc.hbrBackground = hbr;
	wc.lpszMenuName = MAKEINTRESOURCE(IDR_MENU1);
	wc.cbClsExtra = 0;
	wc.cbWndExtra = 0;

	LPTSTR str = L"Close file";
	g_CloseFile.cbSize = sizeof(MENUITEMINFO);
	g_CloseFile.fMask = MIIM_STATE | MIIM_TYPE | MIIM_SUBMENU | MIIM_ID;
	g_CloseFile.fType = MFT_STRING;
	g_CloseFile.fState = MFS_ENABLED;
	g_CloseFile.dwTypeData = str;
	g_CloseFile.cch = sizeof(str);
	g_CloseFile.wID = IDM_FILE_EXIT;
	g_CloseFile.hSubMenu = NULL;


	if (!RegisterClassEx(&wc))
	{
		MessageBox(NULL, TEXT("Ошибка регистрации класса окна!"),
			TEXT("Ошибка"), MB_OK | MB_ICONERROR);
		return FALSE;
	}

	hWnd = CreateWindowEx(
		NULL,
		name,
		title,
		WS_OVERLAPPED | WS_CAPTION | WS_SYSMENU | WS_THICKFRAME,
		NULL,
		NULL,
		800,
		600,
		NULL,
		g_hMenu,
		hInstance,
		NULL
	);

	ShowWindow(hWnd, nCmdShow);
	UpdateWindow(hWnd);


	HACCEL hAccel;
	hAccel = LoadAccelerators(h_hInst,
		MAKEINTRESOURCE(IDR_ACCELERATOR1));


	while (GetMessage(&msg, NULL, 0, 0)) // получение сообщения из очереди 
	{
		if (!TranslateAccelerator(hWnd, hAccel, &msg))
		{
			TranslateMessage(&msg);
			DispatchMessage(&msg);
		}
		else {
			DispatchMessage(&msg);
		}
	}


	return msg.wParam;
}



LRESULT CALLBACK Pr2_WndProc(HWND hWnd, UINT msg, WPARAM wParam, LPARAM lParam)
{
	
	LPCWSTR lpszMsgSpace;
	TCHAR lpszBUFF;


	switch (msg)
	{
		HANDLE_MSG(hWnd, WM_CREATE, WndProc_OnCreate);
		HANDLE_MSG(hWnd, WM_COMMAND, WndProc_COMMAND);
		HANDLE_MSG(hWnd, WM_MENUSELECT, WndProc_MENUSELECT);
		HANDLE_MSG(hWnd, WM_RBUTTONDOWN, WndProc_RBUTTONDOWN);
		HANDLE_MSG(hWnd, WM_DESTROY, WndProc_OnDestroy);


	/*case WM_CREATE:
	{
		InsertMenuItem(g_hMenu, ID_FILE_OPEN, FALSE, &g_CloseFile);
	}
	case WM_COMMAND:
	{
		switch (LOWORD(wParam))
		{
			case ID_FILE_CREATE:
			{
				MessageBox(hWnd, TEXT("Выбран пункт 'CREATE'"), TEXT("Меню File"), MB_OK);
				EnableMenuItem(g_hMenu, ID_EDIT_SELECT, MF_BYCOMMAND | MFS_ENABLED);
				break;
			}
			case ID_FILE_OPEN:
			{
				MessageBox(hWnd, TEXT("Выбран пункт 'OPEN'"), TEXT("Меню File"), MB_OK);
				break;
			}
			case ID_EDIT_SELECT:
			{
				EnableMenuItem(g_hMenu, ID_EDIT_COPY, MF_BYCOMMAND | MFS_ENABLED);
				break;
			}
			case IDM_FILE_EXIT:
			{
				EnableMenuItem(g_hMenu, ID_EDIT_COPY, MF_BYCOMMAND | MFS_DISABLED);
				EnableMenuItem(g_hMenu, ID_EDIT_SELECT, MF_BYCOMMAND | MFS_DISABLED);
				break;
			}
			case ID_ACCELERATOR1:
			{
				MessageBox(hWnd, TEXT("Выбран пункт 'CREATE'"), TEXT("Меню File"), MB_OK);
				break;
			}
			case ID_ACCELERATOR2:
			{
				MessageBox(hWnd, TEXT("Выбран пункт 'OPEN'"), TEXT("Меню File"), MB_OK);
				break;
			}
			case ID_FILE_EXIT:
			{
				DestroyWindow(hWnd);
				break;
			}
			default:
			{
				TCHAR lpszBUFF1[100];
				ZeroMemory(&lpszBUFF1, sizeof(&lpszBUFF));
				wsprintf(lpszBUFF1, TEXT("Обработчик для команды %d отсутствует"), (int)LOWORD(wParam));
				MessageBox(hWnd, lpszBUFF1, TEXT("Menu FILE"), MB_OK | MB_ICONINFORMATION);

				break;
			}
		}
	}
	case WM_MENUSELECT:
	{
		InvalidateRect(hWnd, NULL, TRUE);
		UpdateWindow(hWnd);
		HDC hdc1;
		lpszMsgSpace = TEXT("");
		TCHAR Buf[300];
		HINSTANCE hInst;
		hInst = (HINSTANCE)GetWindowLong(hWnd, GWL_HINSTANCE);
		int size;
		size = LoadString(hInst, LOWORD(wParam), Buf, 300);
		hdc1 = GetDC(hWnd);

		RECT rc;
		GetClientRect(hWnd, &rc);
		TextOut(hdc1, rc.left + 10, rc.bottom - 30,
			lpszMsgSpace, lstrlen(lpszMsgSpace));
		TextOut(hdc1, rc.left + 10, rc.bottom - 30, Buf, lstrlen(Buf));
		ReleaseDC(hWnd, hdc1);
		break;
	}
	case WM_RBUTTONDOWN:
	{
		int x = LOWORD(lParam);
		int y = HIWORD(lParam);

		

		HMENU editMenu = CreatePopupMenu();

		MENUITEMINFO CloseFile;
		LPTSTR str1 = L"Copy";
		LPTSTR str2 = L"Paste";

		CloseFile.cbSize = sizeof(MENUITEMINFO);
		CloseFile.fMask = MIIM_STATE | MIIM_SUBMENU | MIIM_ID;
		GetMenuItemInfo(g_hMenu, ID_EDIT_SELECT, FALSE, &CloseFile);
		AppendMenu(editMenu, CloseFile.fState, ID_EDIT_SELECT, str1);

		CloseFile.cbSize = sizeof(MENUITEMINFO);
		CloseFile.fMask = MIIM_STATE | MIIM_SUBMENU | MIIM_ID;
		GetMenuItemInfo(g_hMenu, ID_EDIT_COPY, FALSE, &CloseFile);
		AppendMenu(editMenu, CloseFile.fState, ID_EDIT_COPY, str2);

		TrackPopupMenuEx(editMenu, TPM_LEFTALIGN | TPM_TOPALIGN, x, y, hWnd, NULL);
		DefWindowProc(hWnd, msg, wParam, lParam);

	}
	break;

	case WM_DESTROY:
		PostQuitMessage(0);
		break;
		*/

	
	default:
		return(DefWindowProc(hWnd, msg, wParam, lParam));;
	}


	return FALSE;
}


BOOL WndProc_OnCreate(HWND hWnd, LPCREATESTRUCT lpCreateStruct)
{
	InsertMenuItem(g_hMenu, ID_FILE_OPEN, FALSE, &g_CloseFile);
	return TRUE;
}

void WndProc_COMMAND(HWND hWnd, int id, HWND hwndCtl, UINT codeNotify)
{
	LPCWSTR lpszMsgSpace;
	TCHAR lpszBUFF;

	switch (LOWORD(id))
	{
		case ID_FILE_CREATE:
		{
			MessageBox(hWnd, TEXT("Выбран пункт 'CREATE'"), TEXT("Меню File"), MB_OK);
			EnableMenuItem(g_hMenu, ID_EDIT_SELECT, MF_BYCOMMAND | MFS_ENABLED);
			break;
		}
		case ID_FILE_OPEN:
		{
			MessageBox(hWnd, TEXT("Выбран пункт 'OPEN'"), TEXT("Меню File"), MB_OK);
			break;
		}
		case ID_EDIT_SELECT:
		{
			EnableMenuItem(g_hMenu, ID_EDIT_COPY, MF_BYCOMMAND | MFS_ENABLED);
			break;
		}
		case IDM_FILE_EXIT:
		{
			EnableMenuItem(g_hMenu, ID_EDIT_COPY, MF_BYCOMMAND | MFS_DISABLED);
			EnableMenuItem(g_hMenu, ID_EDIT_SELECT, MF_BYCOMMAND | MFS_DISABLED);
			break;
		}
		case ID_ACCELERATOR1:
		{
			MessageBox(hWnd, TEXT("Выбран пункт 'CREATE'"), TEXT("Меню File"), MB_OK);
			break;
		}
		case ID_ACCELERATOR2:
		{
			MessageBox(hWnd, TEXT("Выбран пункт 'OPEN'"), TEXT("Меню File"), MB_OK);
			break;
		}
		case ID_FILE_EXIT:
		{
			DestroyWindow(hWnd);
			break;
		}
		default:
		{
			TCHAR lpszBUFF1[100];
			ZeroMemory(&lpszBUFF1, sizeof(&lpszBUFF));
			wsprintf(lpszBUFF1, TEXT("Обработчик для команды %d отсутствует"), (int)LOWORD(id));
			MessageBox(hWnd, lpszBUFF1, TEXT("Menu FILE"), MB_OK | MB_ICONINFORMATION);
			break;
		}
	}
}


void WndProc_MENUSELECT(HWND hWnd, HMENU hmenu, int item, HMENU hmenuPopup, UINT flags)
{
	LPCWSTR lpszMsgSpace;

	InvalidateRect(hWnd, NULL, TRUE);
	UpdateWindow(hWnd);
	HDC hdc1;
	lpszMsgSpace = TEXT("");
	TCHAR Buf[300];

	HINSTANCE hInst;
	hInst = (HINSTANCE)GetWindowLong(hWnd, GWL_HINSTANCE);

	int size;
	size = LoadString(hInst, LOWORD(item), Buf, 300);
	hdc1 = GetDC(hWnd);

	RECT rc;
	GetClientRect(hWnd, &rc);
	TextOut(hdc1, rc.left + 10, rc.bottom - 30,
		lpszMsgSpace, lstrlen(lpszMsgSpace));
	TextOut(hdc1, rc.left + 10, rc.bottom - 30, Buf, lstrlen(Buf));
	ReleaseDC(hWnd, hdc1);

}

void WndProc_RBUTTONDOWN(HWND hWnd, BOOL fDoubleClick, int x, int y, UINT keyFlags)
{

	HMENU submenu = CreatePopupMenu();
	HMENU hmenuBar = GetMenu(hWnd);
	MENUITEMINFO CloseFile;
	LPTSTR str1 = L"Copy";
	LPTSTR str2 = L"Paste";
	CloseFile.cbSize = sizeof(MENUITEMINFO);
	CloseFile.fMask = MIIM_STATE | MIIM_SUBMENU | MIIM_ID;
	GetMenuItemInfo(hmenuBar, ID_EDIT_SELECT, false, &CloseFile);
	AppendMenu(submenu, CloseFile.fState, ID_EDIT_SELECT, str1);
	CloseFile.cbSize = sizeof(MENUITEMINFO);
	CloseFile.fMask = MIIM_STATE | MIIM_SUBMENU | MIIM_ID;
	GetMenuItemInfo(hmenuBar, ID_EDIT_SELECT, false, &CloseFile);
	AppendMenu(submenu, CloseFile.fState, ID_EDIT_SELECT, str2);
	TrackPopupMenuEx(submenu, TPM_LEFTALIGN | TPM_TOPALIGN, x, y, hWnd, NULL);
	FORWARD_WM_RBUTTONDOWN(hWnd, fDoubleClick, x, y, keyFlags, DefWindowProc);
}


void WndProc_OnDestroy(HWND hWnd)
{
	MessageBox(hWnd, TEXT("Shut down programm"), L"Exit", MB_OK);
	PostQuitMessage(0);
}
