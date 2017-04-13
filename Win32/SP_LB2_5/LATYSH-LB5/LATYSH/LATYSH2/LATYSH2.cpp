// LATYSH2.cpp : Defines the entry point for the application.
//
#include "stdafx.h"
#include "LATYSH2.h"
#include <windows.h>
#include <windowsx.h>
#include "resource.h"


static HANDLE s_hFileMap = NULL;

static HWND hWnd;
static LPVOID lpView;

BOOL Dlg_OnInitDialog(HWND hwnd, HWND hwndFocus, LPARAM lParam);
void Dlg_OnCommand(HWND hwnd, int id, HWND hwndCtl, UINT codeNotify);
BOOL CALLBACK Dlg_Proc(HWND hDlg, UINT uMsg, WPARAM wParam, LPARAM lParam);

int WINAPI WinMain(
	HINSTANCE hinstExe,
	HINSTANCE hinstPrev,
	LPSTR lpszCmdLine,
	int nCrndShow)
{
	DialogBox(hinstExe, MAKEINTRESOURCE(IDD_DIALOG1), NULL, (DLGPROC)Dlg_Proc);
	return 0;
}

BOOL CALLBACK Dlg_Proc(HWND hDlg, UINT uMsg, WPARAM wParam, LPARAM lParam)
{
	BOOL fProcessed = TRUE;
	switch (uMsg)
	{
		HANDLE_MSG(hDlg, WM_INITDIALOG, Dlg_OnInitDialog);
		HANDLE_MSG(hDlg, WM_COMMAND, Dlg_OnCommand);

	default:
		fProcessed = FALSE;
		break;
	}
	return fProcessed;
}

BOOL Dlg_OnInitDialog(HWND hWnd, HWND hwndFocus, LPARAM lParam)
{
	HINSTANCE hInst = (HINSTANCE)GetWindowLong(hWnd, GWL_HINSTANCE);
	HWND hEditWnd = GetDlgItem(hWnd, IDC_EDIT);
	Edit_SetText(hEditWnd, __TEXT("Это стартовый текст"));
	s_hFileMap = CreateFileMapping(
		(HANDLE)0xFFFFFFFF,
		NULL,
		PAGE_READWRITE,
		0,
		4 * 1024,
		__TEXT("MMFShareData"));
	if (s_hFileMap != NULL) 
	{
		lpView = MapViewOfFile(s_hFileMap,
			FILE_MAP_READ | FILE_MAP_WRITE,
			0, 0,
			4 * 1024);
		if ((BYTE*)lpView != NULL)
		{
			Edit_GetText(GetDlgItem(hWnd, IDC_EDIT),
				(LPTSTR)lpView, 4 * 1024);
		}
	}
	else
	{
		MessageBox(NULL, __TEXT("Ошибка получения описателя окна проекции"),
			__TEXT("Ошибка"), MB_OK);
		return 0;
	}
	return (TRUE);
}

void Dlg_OnCommand(HWND hwnd, int id, HWND hwndCtl, UINT codeNotify)
{
	HANDLE hFileMapT;
	switch (id)
	{
	case IDC_BUTTON_READ:
	{
		if ((BYTE *)lpView != NULL)
			Edit_SetText(GetDlgItem(hwnd, IDC_EDIT), (LPTSTR)lpView);
		else
			MessageBox(hwnd, __TEXT("Окно не спроецировано"), NULL, MB_OK);
	}
	break;

	case IDC_BUTTON_WRITE:
	{
		if ((BYTE *)lpView != NULL)
			Edit_GetText(GetDlgItem(hwnd, IDC_EDIT), (LPTSTR)lpView, 4 * 1024);
		else
			MessageBox(hwnd, __TEXT("Окно не спроецировано"), NULL, MB_OK);
	}
	break;

	case IDC_BREAK:
	{
		CloseHandle(s_hFileMap);
		UnmapViewOfFile((LPVOID)lpView);
		EndDialog(hwnd, id);
	}
	break;
	}
}

