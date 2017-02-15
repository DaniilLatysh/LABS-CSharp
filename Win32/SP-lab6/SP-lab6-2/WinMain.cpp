//������������ ������ �6 ������2
#include <Windows.h>
#include <tchar.h>
#include <Windowsx.h>
#include "resource.h"

//---���������� ������� ���� ��� ���������
LRESULT CALLBACK Pr2_WndProc(HWND, UINT, WPARAM, LPARAM);
//BOOL TextOut(HDC hDC, int nXStart, int nYStart, LPCTSTR lpString, int cbString);//���:
//HDC hDC ���������� ��������� ���������� �����������
//int nXStart � int nYStart - ���������� ����� ������ ����� ������
//LPCTSTR lpString - ��������� �� ��������� ������
//int cbString - ���������� ��������� ��������
//BOOL WINAPI LineTo(HDC, int, int);//���������� ������� ��� ��������� �����
//���������� ������� ����������� ����
BOOL CALLBACK DlgProc(HWND hWnd, UINT message, WPARAM wParam, LPARAM lParam);
//���������� ������� OnLButtonDown
//void  OnLButtonDown(HWND hwnd, BOOL fDoubleClick, int x, int y,UINT keyFlags); 
//���������� ������� OnLButtonUp
//void  OnLButtonUp(HWND hwnd, int x, int y,UINT keyFlags); 
//���������� ������� OnMouseMove
//void  OnMouseMove(HWND hwnd, int x, int y,UINT keyFlags);
//������� �������� �������� ����
//HWND Create(HINSTANCE, int);
//--------------------------------------------
//---���������� ���������� ����������---
#define g_lpszClassName TEXT("����� ������������")
#define g_lpszAplicationTitle TEXT("������� ���� ����������. ����������� �.�.���������")
HINSTANCE hInstance;
HWND hWnd_dial = NULL;

//------��������� �������-----------
int APIENTRY _tWinMain(
	HINSTANCE hInstance,
	HINSTANCE hPrevInstance,
	LPTSTR lpszCmdLine,
	int nCmdShow)
{
	WNDCLASSEX wc;
	MSG msg;
	HWND hWnd;
	//---��������� ���� ���������----
	memset(&wc, 0, sizeof(WNDCLASSEX));
	wc.cbSize = sizeof(WNDCLASSEX);
	wc.lpszClassName = g_lpszClassName;
	wc.lpfnWndProc = Pr2_WndProc;
	wc.style = CS_VREDRAW | CS_HREDRAW;
	wc.hInstance = hInstance;
	wc.hIcon = LoadIcon(NULL, MAKEINTRESOURCE(IDI_ERROR));
	wc.hCursor = LoadCursor(NULL, MAKEINTRESOURCE(IDC_HAND));
	wc.hbrBackground = CreateSolidBrush(RGB(255, 255, 255));
	wc.lpszMenuName = NULL;
	wc.cbClsExtra = 0;
	wc.cbWndExtra = 0;
	//----�������� ����������� ������ ����---------
	if (!RegisterClassEx(&wc))
	{
		MessageBox(NULL, TEXT("������ ����������� ������ ����"), TEXT("������"), MB_OK | MB_ICONERROR);
		return FALSE;
	}
	//-----�������� ����--------
	hWnd = CreateWindowEx(NULL,
		g_lpszClassName,
		g_lpszAplicationTitle,
		WS_OVERLAPPED | WS_CAPTION | WS_SYSMENU | WS_THICKFRAME | WS_MAXIMIZEBOX | WS_MINIMIZEBOX | WS_CLIPCHILDREN,
		50, 50, 900, 600,
		NULL,
		NULL,
		hInstance,
		NULL);
	//---�������� �������� ����----
	if (!hWnd)
	{
		MessageBox(NULL, TEXT("���� �� �������!"), TEXT("������"), MB_OK | MB_ICONERROR);
		return FALSE;
	}
	ShowWindow(hWnd, nCmdShow);//��� ��� ���� ������� ������ � ������ (��� ��������� WS_VISIBLE) ������� ShowWindow ��������� ��� �� ������� ��������� nCmdShow � ���������� 
	UpdateWindow(hWnd);//������� �������� ������� ���� � ������������� ����������� ����

					   //---����������� ����� ��������� ���������-------
					   //&msg- ��������� �� ��������� � ������� ����� �������� ���������
					   //NULL- ���������� ���� ��� �������� ����� �������� ��������� (� ������ ������ ��� ���� ����)
					   //0 � 0 ��� �������� ������� � ������ ������ ��� ��� ���������
	while (GetMessage(&msg, NULL, 0, 0))
	{
		TranslateMessage(&msg);//������� �������� � ���������� ��������
		DispatchMessage(&msg);//������������ ��������� �� �������� ����, ����� � ���������� ����� ������� ����
	}
	return msg.wParam;
}

//------������� ���� ��� ��������� ����------
LRESULT CALLBACK Pr2_WndProc(HWND hWnd, UINT msg, WPARAM wParam, LPARAM lParam)
{
	TCHAR BUFF[250];//��������������� �����
	HDC hdc;                       // ���������� ��������� ����������
	RECT rcClient;                 // ������������� ���������� ������� 
	POINT ptClientUL;              // ���������� ������� ����� ����
	POINT ptClientLR;              // ���������� ������ ������ ���� 
	static POINTS ptsBegin;        // ��������� �����
	static POINTS ptsEnd;          // ����� �������� ����� 
	static POINTS ptsPrevEnd;      // ���������� �������� ����� 
	static BOOL fPrevLine = FALSE; // ���������� ���� ����� 
	static HWND hWnd_dial;
	switch (msg)
	{
	case WM_CREATE:
	{
		HWND hWnd_edit0 = CreateWindow(TEXT("edit"),
			NULL,
			WS_CHILD | WS_VISIBLE | WS_BORDER | ES_LEFT,
			10, 10, 310, 30,
			hWnd,
			(HMENU)ID_EDIT0,
			hInstance,
			NULL);
		//������� ���� �������� �������
		hWnd_dial = CreateDialog((HINSTANCE)GetWindowLong(hWnd, GWL_HINSTANCE),
			MAKEINTRESOURCE(IDD_DIALOG1), hWnd, (DLGPROC)DlgProc);
		//������������� ����������� ������� ���� � ��� ��������
		SetWindowPos(hWnd_dial, 0, 90, 190, 150, 300, 0);
		//���������� ����
		ShowWindow(hWnd_dial, SW_SHOW);
		return 0;
	}
	case WM_KEYDOWN:
	{
		static RECT dl;//��������� ��� ������ �������������� ����������� ����
		static	RECT Wnd;//��������� ��� ������ �������������� ������������� ����
		POINT ptClientUpL;// ���������� ������� ����� ����
		POINT ptClientLowR;// ���������� ������ ������ ���� 
		GetWindowRect(hWnd_dial, &dl);//��������� ���������
		GetWindowRect(hWnd, &Wnd);//��������� ���������
		ptClientUpL.x = dl.left;//���� x ��� ����
		ptClientUpL.y = dl.top;//���� y ��� ����
		ptClientLowR.x = dl.right;//���� x ��� ����
		ptClientLowR.y = dl.bottom;//���� x ��� ����
		ScreenToClient(hWnd, &ptClientUpL);//������� �� �������� � ���������� 
		ScreenToClient(hWnd, &ptClientLowR);//������� �� �������� � ���������� 
		TCHAR buf[20];//��������������� �����
		wsprintf(buf, TEXT("X = %d, Y = %d "), dl.left - Wnd.left, dl.top - Wnd.top);//���������� � ����� ����������
		HDC hdc = GetDC(hWnd);//����� �������� �������� ����
		SetTextColor(hdc, RGB(255, 128, 0));//������������� ���� ������
		int d = 10;
		TextOut(hdc, 30, Wnd.bottom - 120, (LPCWSTR)buf, wcslen(buf));//����� ������

		switch (LOWORD(wParam))
		{
		case 'B'://����� ����������� ���� �����
		{
			MoveWindow(hWnd_dial,
				ptClientUpL.x - d,
				ptClientUpL.y,
				ptClientLowR.x - ptClientUpL.x,
				ptClientLowR.y - ptClientUpL.y,
				TRUE);
			break;
		}
		case 'N'://����� ����������� ���� ������
		{
			MoveWindow(hWnd_dial,
				ptClientUpL.x + d,
				ptClientUpL.y,
				ptClientLowR.x - ptClientUpL.x,
				ptClientLowR.y - ptClientUpL.y,
				TRUE);
			break;
		}
		case 'M'://����� ����������� ���� �����
		{
			MoveWindow(hWnd_dial,
				ptClientUpL.x,
				ptClientUpL.y - d,
				ptClientLowR.x - ptClientUpL.x,
				ptClientLowR.y - ptClientUpL.y,
				TRUE);
			break;
		}
		case '<'://����� ����������� ���� ����
		{
			MoveWindow(hWnd_dial,
				ptClientUpL.x,
				ptClientUpL.y + d,
				ptClientLowR.x - ptClientUpL.x,
				ptClientLowR.y - ptClientUpL.y,
				TRUE);
			break;
		}
		case VK_F2:
		{
			// Process the F2 key. 
			break;
		}
		case VK_F3:
		{
			// Process the F2 key. 
			break;
		}
		default:
		{
			break; 
		}
		}
		return 0;
	}
	case WM_LBUTTONDOWN:
	{
		// ������ ����� ���� 
		SetCapture(hWnd);

		// ��������� ���������� ��������� ������� ��� ���������� �������,
		// � �������������� �� � ���������� ����������.. 
		GetClientRect(hWnd, &rcClient);
		ptClientUL.x = rcClient.left;
		ptClientUL.y = rcClient.top;

		// ��������� �� ������� � ������ � ������ ��������,
		// ������ ��� ����������, ���������� �����  GetClientRect 
		// �� �������� ����� ����� � ����� ������ �������.

		ptClientLR.x = rcClient.right + 1;
		ptClientLR.y = rcClient.bottom + 1;
		ClientToScreen(hWnd, &ptClientUL);
		ClientToScreen(hWnd, &ptClientLR);

		// �������� ��������� ���������� ��������� ������� � ��������� rcClient 
		// ���������  ��������� rcClient  ������� ClipCursor ��������� 
		// ����������� ������� ���� ���������� ��������.
		SetRect(&rcClient, ptClientUL.x, ptClientUL.y,
			ptClientLR.x, ptClientLR.y);
		ClipCursor(&rcClient);

		// ������������ ���������� ������� � ��������� POINTS, 
		// ������� ������ ��������� ����� �����, 
		// ��������� ��� ��������� ��������� WM_MOUSEMOVE. 
		ptsBegin = MAKEPOINTS(lParam);
		SetWindowText(GetDlgItem(hWnd, 1001), TEXT("\0"));//�������� edit
		memset(BUFF, 0, sizeof(&BUFF));//�������� �����
		wsprintf(BUFF, TEXT("���������� ����: X=%d : Y=%d"), ptsBegin.x, ptsBegin.y);
		SendMessage(GetDlgItem(hWnd, 1001), EM_REPLACESEL, 0, (LPARAM)(BUFF));
		return 0;
	}
	case WM_MOUSEMOVE:
	{
		// ��� ��������� ������ ��� ����������� ���� ����� ����������
		// ������� ����� ������ ���� 
		if (wParam & MK_LBUTTON)
		{
			// �������� �������� ���������� ��� ��������� ������� 
			hdc = GetDC(hWnd);
			// ��������� ������� ������������ ����� ������ 
			//  ����� ������������ ����� ����� ������, � ������ ����� ����� ������ 
			SetROP2(hdc, R2_NOTXORPEN);

			// ���� ����� ���� �� �����, ������������ ��� ��������� �����������
			// ���������  WM_MOUSEMOVE, �� ��������� �������������� ������. 
			// ��������� ����� � ������ �������� � �������� ����� 
			if (fPrevLine)
			{
				MoveToEx(hdc, ptsBegin.x, ptsBegin.y,
					(LPPOINT)NULL);
				LineTo(hdc, ptsPrevEnd.x, ptsPrevEnd.y);
			}

			// ������������� ������� ��������� ������� � ��������� 
			// POINTS  � ��������� ����� �����. 

			ptsEnd = MAKEPOINTS(lParam);
			SetWindowText(GetDlgItem(hWnd, 1001), TEXT("\0"));//�������� edit
			memset(BUFF, 0, sizeof(&BUFF));//�������� �����
			wsprintf(BUFF, TEXT("���������� ����: X=%d : Y=%d"), ptsEnd.x, ptsEnd.y);
			SendMessage(GetDlgItem(hWnd, 1001), EM_REPLACESEL, 0, (LPARAM)(BUFF));
			MoveToEx(hdc, ptsBegin.x, ptsBegin.y, (LPPOINT)NULL);
			LineTo(hdc, ptsEnd.x, ptsEnd.y);

			//  ������������� ���� ���������� �����, ��������� �������� �����
			//  ����� ����� � �����������  �������� ���������� 
			fPrevLine = TRUE;
			ptsPrevEnd = ptsEnd;
			ReleaseDC(hWnd, hdc);
		}
		return 0;
	}
	case WM_LBUTTONUP:
	{
		// ������������ �������� ��������� �����. 
		// ���������� ���� ���������� �����,
		//  ����������� ������ � ����������� ������ ���� 
		fPrevLine = FALSE;
		ClipCursor(NULL);
		ReleaseCapture();
		return 0;
	}
	case WM_DESTROY:
	{
		PostQuitMessage(0);
		return 0;
	}
	default:
	{
		return(DefWindowProc(hWnd, msg, wParam, lParam));
	}

	}
	return 0;
}


BOOL CALLBACK DlgProc(HWND hWnd, UINT message, WPARAM wParam, LPARAM lParam)
{
	HDC hdc;                       // ���������� ��������� ����������
	RECT rcClient;                 // ������������� ���������� ������� 
	POINT ptClientUL;              // ���������� ������� ����� ����
	POINT ptClientLR;              // ���������� ������ ������ ���� 
	static POINTS ptsBegin;        // ��������� �����
	static POINTS ptsEnd;          // ����� �������� ����� 
	static POINTS ptsPrevEnd;      // ���������� �������� ����� 
	static BOOL fPrevLine = FALSE; // ���������� ���� ����� 


	switch (message)
	{
		// Place message cases here. 

	case WM_LBUTTONDOWN:

		// ������ ����� ���� 
		SetCapture(hWnd);

		// ��������� ���������� ��������� ������� ��� ���������� �������,
		// � �������������� �� � ���������� ����������.. 
		GetClientRect(hWnd, &rcClient);
		ptClientUL.x = rcClient.left;
		ptClientUL.y = rcClient.top;

		// ��������� �� ������� � ������ � ������ ��������,
		// ������ ��� ����������, ���������� �����  GetClientRect 
		// �� �������� ����� ����� � ����� ������ �������.

		ptClientLR.x = rcClient.right + 1;
		ptClientLR.y = rcClient.bottom + 1;
		ClientToScreen(hWnd, &ptClientUL);
		ClientToScreen(hWnd, &ptClientLR);

		// �������� ��������� ���������� ��������� ������� � ��������� rcClient 
		// ���������  ��������� rcClient  ������� ClipCursor ��������� 
		// ����������� ������� ���� ���������� ��������.
		SetRect(&rcClient, ptClientUL.x, ptClientUL.y,
			ptClientLR.x, ptClientLR.y);
		ClipCursor(&rcClient);

		// ������������ ���������� ������� � ��������� POINTS, 
		// ������� ������ ��������� ����� �����, 
		// ��������� ��� ��������� ��������� WM_MOUSEMOVE. 
		ptsBegin = MAKEPOINTS(lParam);
		return 0;

	case WM_MOUSEMOVE:

		// ��� ��������� ������ ��� ����������� ���� ����� ����������
		// ������� ����� ������ ���� 
		if (wParam & MK_LBUTTON)
		{
			// �������� �������� ���������� ��� ��������� ������� 
			hdc = GetDC(hWnd);
			// ��������� ������� ������������ ����� ������ 
			//  ����� ������������ ����� ����� ������, � ������ ����� ����� ������ 
			SetROP2(hdc, R2_NOTXORPEN);

			// ���� ����� ���� �� �����, ������������ ��� ��������� �����������
			// ���������  WM_MOUSEMOVE, �� ��������� �������������� ������. 
			// ��������� ����� � ������ �������� � �������� ����� 
			if (fPrevLine)
			{
				MoveToEx(hdc, ptsBegin.x, ptsBegin.y,
					(LPPOINT)NULL);
				LineTo(hdc, ptsPrevEnd.x, ptsPrevEnd.y);
			}

			// ������������� ������� ��������� ������� � ��������� 
			// POINTS  � ��������� ����� �����. 

			ptsEnd = MAKEPOINTS(lParam);
			MoveToEx(hdc, ptsBegin.x, ptsBegin.y, (LPPOINT)NULL);
			LineTo(hdc, ptsEnd.x, ptsEnd.y);

			//  ������������ ���� ���������� �����, ��������� �������� �����
			//  ����� ����� � �����������  �������� ���������� 
			fPrevLine = TRUE;
			ptsPrevEnd = ptsEnd;
			ReleaseDC(hWnd, hdc);
		}
		break;

	case WM_LBUTTONUP:

		// ������������ �������� ��������� �����. 
		// ���������� ���� ���������� �����,
		//  ����������� ������ � ����������� ������ ���� 
		fPrevLine = FALSE;
		ClipCursor(NULL);
		ReleaseCapture();
		return 0;

		//////////////////////////

	default: return FALSE;

	} return FALSE;
}