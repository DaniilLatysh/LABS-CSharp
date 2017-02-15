/*
*                       ������������ ������: 6 ������ 2
*
*/

#include <Windows.h>
#include <tchar.h>
#include <Windowsx.h>
#include "resource.h"

//---���������� ������� ���� ��� ���������
LRESULT CALLBACK Pr2_WndProc(HWND, UINT, WPARAM, LPARAM);

//---���������� ���������� ����������---
#define g_lpszClassName TEXT("���������������� �����")
#define g_lpszAplicationTitle TEXT("������� ���� ����������. ����������� ����� �.�.")
HINSTANCE hInstance;

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
		WS_OVERLAPPED | WS_CAPTION | WS_SYSMENU | WS_THICKFRAME | WS_MAXIMIZEBOX | WS_MINIMIZEBOX,
		200, 200, 800, 300,
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
	ShowWindow(hWnd, nCmdShow);
	UpdateWindow(hWnd);

	while (GetMessage(&msg, NULL, 0, 0))
	{
		TranslateMessage(&msg);
		DispatchMessage(&msg);
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
		break;
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
	{ PostQuitMessage(0);
	break; }
	default:
	{return(DefWindowProc(hWnd, msg, wParam, lParam));
	break; }
	}
	return 0;
}
