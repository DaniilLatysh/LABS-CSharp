#include <Windows.h>
#include <tchar.h>

#define g_lpszClassName TEXT("sp_pr2_class");
#define g_lpszAplicationTitle TEXT(" ������� ���� ����������. ���������� ����� ������");
#define g_lpszDestroyMessage TEXT("����������� ����. � ����� � ���� ��������� ��������� WM_DESTROY, �� ����������� �������� � �������� ������ �����.");
#define ID_BUTTON 1

HINSTANCE h_hInst;
HWND g_hListBox;
LRESULT CALLBACK Pr2_WndProc(HWND, UINT, WPARAM, LPARAM);
LRESULT CALLBACK ChildProc(HWND, UINT, WPARAM, LPARAM);

int APIENTRY _tWinMain(HINSTANCE hInstance, HINSTANCE hPrevInstance, LPTSTR lpszCmdLine, int nCmdShow)
{
	LPCTSTR name = g_lpszClassName;
	LPCTSTR title = g_lpszAplicationTitle;
	HWND hWnd;
	WNDCLASSEX wc;
	MSG msg;
	h_hInst = hInstance;

	HBRUSH hbr = CreateSolidBrush(RGB(0, 127, 127));
	memset(&wc, 0, sizeof(WNDCLASSEX));
	wc.cbSize = sizeof(WNDCLASSEX);
	wc.lpszClassName = name;
	wc.lpfnWndProc = Pr2_WndProc;
	wc.style = CS_VREDRAW | CS_HREDRAW;
	wc.hInstance = hInstance;
	wc.hIcon = LoadIcon(NULL, MAKEINTRESOURCE(IDI_WINLOGO));
	wc.hCursor = LoadCursor(NULL, MAKEINTRESOURCE(IDC_ARROW));
	wc.hbrBackground = hbr;
	wc.lpszMenuName = NULL;
	wc.cbClsExtra = 0;
	wc.cbWndExtra = 0;


	if (!RegisterClassEx(&wc))
	{
		MessageBox(NULL, TEXT("������ ����������� ������ ����!"),
			TEXT("������"), MB_OK | MB_ICONERROR);
		return FALSE;
	}

	hWnd = CreateWindowEx(
		NULL,
		name,
		title,
		WS_OVERLAPPED | WS_CAPTION | WS_SYSMENU | WS_THICKFRAME,
		100,
		100,
		800,
		600,
		NULL,
		NULL,
		hInstance,
		NULL
	);

	if (!(g_hListBox = CreateWindowEx(
		0L,
		TEXT("ListBox"),
		TEXT("������"),
		WS_CHILD | WS_BORDER | WS_VISIBLE,
		400, 0, 400, 150,
		hWnd,
		(HMENU)103,
		h_hInst, NULL))
		) return (-1);

	ShowWindow(hWnd, nCmdShow);
	UpdateWindow(hWnd);

	while (GetMessage(&msg, NULL, 0, 0)) // ��������� ��������� �� ������� 
	{
		TranslateMessage(&msg);
		DispatchMessage(&msg);
		
	}
	return msg.wParam;
}
	










LRESULT CALLBACK Pr2_WndProc(HWND hWnd, UINT msg, WPARAM wParam, LPARAM lParam)
{
	LPCWSTR text = g_lpszDestroyMessage;
	LPCTSTR name = g_lpszClassName;
	LPCTSTR title = g_lpszAplicationTitle;

	static TCHAR pszTextBuff[500];
	static HWND hChildWND;
	static HWND hEdit, hWnd_button;
	static HDC hDC;
	HBRUSH hbr = CreateSolidBrush(RGB(0, 127, 127));

	int x, y;
	x = LOWORD(lParam);
	y = HIWORD(lParam);

	TCHAR text2[] = TEXT("��������� ��������� WM_LBUTTONDOWN, ������� ���������� � ���� ��� ������ ����� ������ ����");
	TCHAR text3[] = TEXT("����� ������ ��� ��������� ��������� WM_PAINT. ��� ���������� ���� �������� ����� ����, ��� ��� ���� ������� ������ ����� � ����� �������.");

	

	switch (msg)
	{
	case WM_CREATE:
	{

		MessageBox(NULL, TEXT("����������� ��������� ��������� WM_CREATE"), TEXT("CREATE"), MB_OK);

		static WNDCLASSEX wc;
		HBRUSH hbr = CreateSolidBrush(RGB(0, 255, 137));
		memset(&wc, 0, sizeof(WNDCLASSEX));
		wc.cbSize = sizeof(WNDCLASSEX);
		wc.lpszClassName = TEXT("mychild");
		wc.lpfnWndProc = ChildProc;
		wc.hInstance = h_hInst;
		wc.hIcon = LoadIcon(NULL, MAKEINTRESOURCE(IDI_WINLOGO));
		wc.hCursor = LoadCursor(NULL, MAKEINTRESOURCE(IDC_ARROW));
		wc.hbrBackground = hbr;



		if (!RegisterClassEx(&wc))
		{
			MessageBox(NULL, TEXT("������ ����������� ������ ����!"),
				TEXT("������"), MB_OK | MB_ICONERROR);
			return -1;
		}


		hChildWND = CreateWindowEx(
			0,
			TEXT("mychild"),
			(LPCTSTR)NULL,
			WS_CHILD | WS_BORDER | WS_CAPTION | WS_SYSMENU | WS_VISIBLE,
			30,
			30,
			300,
			300,
			hWnd,
			(HMENU)101,
			h_hInst,
			NULL
		);
		
		hWnd_button = CreateWindow(TEXT("button"),
			TEXT("������ 0"),
			WS_CHILD | BS_PUSHBUTTON | WS_VISIBLE,
			60, 80, 100, 20,
			hWnd,
			(HMENU)ID_BUTTON,
			h_hInst,
			NULL
		);


		if (!hChildWND)
		{
			MessageBox(NULL, TEXT("���� child �� �������!"),
				TEXT("������"), MB_OK | MB_ICONERROR);
		}

		ShowWindow(hChildWND, SW_SHOW);
		UpdateWindow(hChildWND);

		return 0;
		
	}
	case WM_COMMAND:
	
		if ((HIWORD(wParam) == BN_CLICKED) && (LOWORD(wParam) == ID_BUTTON))
		{
			MessageBox(NULL, TEXT("������ ������"), TEXT("����������"), MB_OK);
		}
		return 0;

	

	case WM_PAINT:
		PAINTSTRUCT ps;
		hDC = BeginPaint(hWnd, &ps);

		TextOut(hDC, 20, 100, text3, ARRAYSIZE(text3));
		EndPaint(hWnd, &ps);
		break;

	case WM_LBUTTONDOWN:
		int ind;
		RECT rct;
		hDC = GetDC(hWnd);

		rct.left = x;
		rct.top = y;
		wsprintf(pszTextBuff, TEXT(" X: %d | Y: %d"), rct.left, rct.top);

		//TextOut(hDC, x, y, text, ARRAYSIZE(text2));  // ����� ������ ��������
		DrawText(hDC, text2, ARRAYSIZE(text2), &rct, NULL); // ����� ������ ��������

		
		ind = SendMessage
		(
			g_hListBox,
			LB_ADDSTRING, 
			0, 
			(LPARAM)pszTextBuff
		); 
		
		if (ind == LB_ERR)
		{
			MessageBox(hWnd, TEXT("������ � ������"), NULL, MB_OK);
		}
	

		break;

	case WM_DESTROY:  // ���������� ������ ����������
		MessageBox(NULL, text,
			TEXT("������"), MB_OK | MB_ICONERROR);
		PostQuitMessage(0); // ������� WM_QUIT ����������
		break;
		
	default: // ����� "����������� �� ���������"
		return(DefWindowProc(hWnd, msg, wParam, lParam));
	}

	return(DefWindowProc(hWnd, msg, wParam, lParam));
}

LRESULT CALLBACK ChildProc(HWND hwnd, UINT Message, WPARAM wparam, LPARAM lparam)
{
	if (Message == WM_DESTROY)
	{
		return 0;
	}
	return DefWindowProc(hwnd, Message, wparam, lparam);
}