#include <Windows.h>
#include <tchar.h>
#define MESSAGE_TEXT TEXT("Hello World Win32 GUI Application");
#define MESSAGE_TITLE TEXT("Message");

//-- Prototypes -------------------
LRESULT CALLBACK SimWndProc(HWND, UINT, WPARAM, LPARAM);

//-- Main function -------------------
int APIENTRY _tWinMain(HINSTANCE hInstance, HINSTANCE hPrevInstance, LPTSTR lpszCmdLine, int nCmdShow)
{

	LPCTSTR lpszHelloText = MESSAGE_TEXT;
	LPCTSTR lpszHelloWindTitle = MESSAGE_TITLE;

	MessageBox(NULL, lpszHelloText, lpszHelloWindTitle, MB_OK);

	TCHAR buff[100], buffRes[100];
	SYSTEMTIME stLocal;
	GetLocalTime(&stLocal);
	

	lstrcpy(buffRes, TEXT(" Latysh 50326-1\n"));

	wsprintf(buff, TEXT(" Design time: 20.12.2016\n"));

	lstrcat(buffRes, buff);

	wsprintf(buff, TEXT(" Run time: %d.%d.%d"), stLocal.wDay, stLocal.wMonth, stLocal.wYear);

	lstrcat(buffRes, buff);

	MessageBox(NULL, buffRes, lpszHelloWindTitle, MB_ICONINFORMATION | MB_ABORTRETRYIGNORE | MB_DEFBUTTON2);



	int iRetValue1, iRetValue2;
	LPCTSTR lpszMesBoxTitle = TEXT("������� MessageBox");
	LPTSTR lpszResponce;
	do {
		iRetValue1 = MessageBox(NULL, TEXT("������� ���� �� ������."), lpszMesBoxTitle,
			MB_ICONINFORMATION | MB_ABORTRETRYIGNORE | MB_DEFBUTTON2);
		switch (iRetValue1)
		{
		case IDABORT:
			lpszResponce = TEXT("������ ������ �������� (ABORT)");
			break;
		case IDRETRY:
			lpszResponce = TEXT("������ ������ ��������� (RETRY)");
			break;
		case IDIGNORE:
			lpszResponce = TEXT("������ ������ ���������� (IGNORE)");
			break;
		default: lpszResponce = TEXT("����� ��� �� �������.");
		}
		TCHAR buf[200] = TEXT("");
		lstrcat(buf, lpszResponce);
		lstrcat(buf, TEXT(" ���������� �������� ������������ ��������?"));
		iRetValue2 = MessageBox(NULL, buf, lpszMesBoxTitle,
			MB_ICONINFORMATION | MB_ABORTRETRYIGNORE | MB_DEFBUTTON2);
	} while (iRetValue2 != IDABORT);


	WNDCLASSEX wc;
	MSG msg;
	HWND hWnd;
	HBRUSH hbr = CreateSolidBrush(RGB(167, 255, 181));

	memset(&wc, 0, sizeof(WNDCLASSEX));
	wc.cbSize = sizeof(WNDCLASSEX);
	wc.lpszClassName = TEXT("SimpleClassName");
	wc.lpfnWndProc = SimWndProc;
	wc.style = CS_VREDRAW | CS_HREDRAW;
	wc.hInstance = hInstance;
	wc.hIcon = LoadIcon(NULL, MAKEINTRESOURCE(IDI_APPLICATION));
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

	hWnd = CreateWindowEx(NULL, TEXT("SimpleClassName"),
		TEXT("Simple Application with Message handling"),
		WS_OVERLAPPEDWINDOW,
		0,
		0,
		CW_USEDEFAULT,
		CW_USEDEFAULT,
		NULL,
		NULL,
		hInstance,
		NULL
	);
	if (!hWnd)
	{
		MessageBox(NULL, TEXT("���� �� �������!"),
			TEXT("������"), MB_OK | MB_ICONERROR);
		return FALSE;
	}

	ShowWindow(hWnd, nCmdShow);
	UpdateWindow(hWnd);

	while (GetMessage(&msg, NULL, 0, 0))
	{
		DispatchMessage(&msg);
	}
	return msg.wParam;
}


// ������� ���������
LRESULT CALLBACK SimWndProc(HWND hWnd, UINT msg,
	WPARAM wParam, LPARAM lParam)
{
	HDC hDC;
	switch (msg)
	{
	case WM_PAINT:    // ����� ��� ���������� ����
		PAINTSTRUCT ps;
		hDC = BeginPaint(hWnd, &ps); // ��������� ��������� ���
									 // ���������� ���� 
		TextOut(hDC, 10, 10, TEXT("Hello, World!"), 13); // ����� � ��������
		EndPaint(hWnd, &ps); // ���������� ���������� ����
		break;

	case WM_DESTROY:  // ���������� ������ ����������
		PostQuitMessage(0); // ������� WM_QUIT ����������
		break;

	default: // ����� "����������� �� ���������"
		return(DefWindowProc(hWnd, msg, wParam, lParam));
	}
	return FALSE;// ��� ������ � "break"
}