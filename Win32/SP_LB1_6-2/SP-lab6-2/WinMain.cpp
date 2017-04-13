#include <windows.h>
#include <windowsx.h>
#include <string.h>
#include  <stdio.h>


LPCTSTR lpszClassName = TEXT("Latysh D.N.");
LPCWSTR lpszAppName = TEXT("Lab work 6");
HWND hMainWnd;
BOOL fDraw = 0;
HDC g_hDC;



LRESULT WINAPI WndProc(HWND, UINT, WPARAM, LPARAM);
BOOL Register(HINSTANCE);
HWND Create(HINSTANCE, int);



int WINAPI WinMain(HINSTANCE hInst, HINSTANCE hPrevInst,
	LPSTR lpszCmdLine, int nCmdShow)
{
	MSG Msg;
	BOOL ok;

	ok = Register(hInst);
	if (!ok) return FALSE;

	hMainWnd = Create(hInst, nCmdShow);
	if (!hMainWnd) return FALSE;
	while (GetMessage(&Msg, NULL, 0, 0))
	{
		TranslateMessage(&Msg);
		DispatchMessage(&Msg);
	}
	return Msg.wParam;
}

BOOL Register(HINSTANCE hinst)
{
	WNDCLASSEX WndClass;
	BOOL fRes;

	memset(&WndClass, 0, sizeof(WNDCLASSEX));
	WndClass.cbSize = sizeof(WNDCLASSEX);
	WndClass.lpszClassName = lpszClassName;
	WndClass.style = CS_HREDRAW | CS_VREDRAW | CS_DBLCLKS;
	WndClass.lpfnWndProc = WndProc;
	WndClass.cbClsExtra = 0;
	WndClass.cbWndExtra = 0;
	WndClass.hInstance = hinst;
	WndClass.hIcon = NULL;
	WndClass.hCursor = NULL;
	WndClass.hbrBackground = (HBRUSH)(COLOR_WINDOW + 1);
	WndClass.lpszMenuName = NULL;

	fRes = (BOOL)RegisterClassEx(&WndClass);
	return fRes;
}

HWND Create(HINSTANCE hInstance, int nCmdShow)
{
	HWND hwnd = CreateWindowEx(
		0,
		lpszClassName,
		lpszAppName,
		WS_OVERLAPPEDWINDOW,
		90, 190,
		150, 300,
		NULL, NULL, hInstance, NULL);
	if (hwnd == NULL) return hwnd;
	ShowWindow(hwnd, nCmdShow);
	UpdateWindow(hwnd);

	g_hDC = GetDC(hwnd);

	return hwnd;
}