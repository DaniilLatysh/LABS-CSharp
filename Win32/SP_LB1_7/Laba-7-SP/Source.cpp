#include <windows.h>
#include <windowsx.h>
#include <tchar.h>
#include <cmath>


//�������� ������� ����
LRESULT CALLBACK PR2_WndProc(HWND, UINT, WPARAM, LPARAM); // �������� ������� ���������
														  
BOOL OnDestroy(HWND hWnd);
BOOL OnCreate(HWND hWnd, LPCREATESTRUCT lpCreateStruct);
BOOL OnPaint(HWND hWnd);
BOOL OnCommand(HWND hWnd, int id, HWND hwndCtl, UINT codeNotify);

//���������� ����������

HWND hWnd;
int wmId;
HDC hdc;
LPCWSTR file_name = L"D:\\MetaFile.emf";
static HDC hdcEMF;
HENHMETAFILE hemf;
ENHMETAHEADER emh;
RECT rect;
PAINTSTRUCT ps;

//  ��������� �������
int APIENTRY _tWinMain(HINSTANCE hInstance,
	HINSTANCE hPrevInstance,
	LPTSTR lpszCmdLine,
	int nCmdShow)
{
	WNDCLASSEX wc;
	MSG msg;
	HWND hWnd;

	memset(&wc, 0, sizeof(WNDCLASSEX));
	wc.cbSize = sizeof(WNDCLASSEX);
	wc.lpszClassName = TEXT("SimpleClassName");
	wc.lpfnWndProc = PR2_WndProc;
	wc.style = CS_VREDRAW | CS_HREDRAW;
	wc.hInstance = hInstance;
	wc.hbrBackground = CreateSolidBrush(RGB(255, 255, 255));
	wc.cbClsExtra = 0;
	wc.cbWndExtra = 0;

	// ���������� ������ � ������, ����� ����������� ������ ��������� � �������
	if (!RegisterClassEx(&wc)) {
		MessageBox(NULL, TEXT("������ ����������� ������ ����!"), TEXT("������"), MB_OK | MB_ICONERROR);
		return FALSE;
	}

	// �������� �������� ����
	hWnd = CreateWindowEx(NULL,
		TEXT("SimpleClassName"),
		TEXT("������������ ������ 7. ����������� ����� �.�."),
		WS_OVERLAPPEDWINDOW,
		100, 100, // ������� ����
		700, 500, // ������� ����
		NULL, NULL, hInstance, NULL);

	// �������� ���������� � ���������� ������ � ������, ����� ���� �� ����� �������
	if (!hWnd) {
		MessageBox(NULL, TEXT("���� �� �������!"), TEXT("������"), MB_OK | MB_ICONERROR);
		return FALSE;
	}

	// ����������� �������� ����
	ShowWindow(hWnd, nCmdShow);
	UpdateWindow(hWnd);

	while (GetMessage(&msg, NULL, 0, 0)) {
		TranslateMessage(&msg);
		DispatchMessage(&msg);
	}

	return msg.wParam;
}

// ������� ����
LRESULT CALLBACK PR2_WndProc(HWND hWnd, UINT msg, WPARAM wParam, LPARAM lParam)
{
	switch (msg)
	{
		HANDLE_MSG(hWnd, WM_CREATE, OnCreate);
		HANDLE_MSG(hWnd, WM_COMMAND, OnCommand);
		HANDLE_MSG(hWnd, WM_DESTROY, OnDestroy);
		HANDLE_MSG(hWnd, WM_PAINT, OnPaint);
	default:
		return(DefWindowProc(hWnd, msg, wParam, lParam));
	}
	return 0;
}

BOOL OnPaint(HWND hWnd) //����������� ������ � ���� � ��������� �����������
{
	hdc = BeginPaint(hWnd, &ps); //�������� ��������
	GetClientRect(hWnd, &rect);	//���������� �������			
	hemf = GetEnhMetaFile(L"D:\\MetaFile.emf"); //�������� ��������
	PlayEnhMetaFile(hdc, hemf, &rect); //������������� ������ ���������	
	DeleteEnhMetaFile(hemf); //������ ��������
	EndPaint(hWnd, &ps); //��������� ��������
	return TRUE;

}

BOOL OnCreate(HWND hWnd, LPCREATESTRUCT lpCreateStruct)
{
	hdcEMF = CreateEnhMetaFile(NULL, L"D:\\MetaFile.emf", NULL, NULL); // �������� �������� ��������� ����������
	static LOGFONT lf; //������ ��������� LOGFONT
	WCHAR lpszFace1[] = TEXT("Microsoft Sans Serif");// �������� ������
	lf.lfCharSet = DEFAULT_CHARSET; //�������� �� ���������
	lf.lfPitchAndFamily = DEFAULT_PITCH; //�������� �� ���������
	wcscpy_s(lf.lfFaceName, lpszFace1); //�������� � ������ �������� ������ 
	lf.lfHeight = -MulDiv(20, GetDeviceCaps(hdcEMF, LOGPIXELSY), 72); //��������� ������ ������ � ��������
	lf.lfItalic = 0; //���������� �������
	HFONT hFont = CreateFontIndirect(&lf); //C������ �����
	SelectObject(hdcEMF, hFont); //�� ����� ����� ���� ������ ����� �� ��� �������
	SetTextColor(hdcEMF, RGB(127, 0, 0)); //������� ���� ������
	SetBkMode(hdcEMF, TRANSPARENT); //���������� ��������
	TextOut(hdcEMF, 30, 50, TEXT("����� ������ ���������"), lstrlen(TEXT("����� ������ ���������"))); //������� �����
	DeleteObject(hFont); //�������� �� ������ ������ ������
						 //����� � ���� ������������ �����������/
	HPEN hPen = CreatePen(PS_SOLID, 3, RGB(60, 60, 0));//���� ��� ������� ����� ��������, ������� 3,�������
	HBRUSH hBrush = CreateHatchBrush(HS_CROSS, RGB(0, 0, 139));//��������� ��������������,�����

	const int cubeX = 150; //������ ��������� ����
	const int cubeY = 200; //������ ��������� ����
	const int A = 100;//������ �
	const int B = 140;//������ �
					  //������ ����
	SelectPen(hdcEMF, hPen); // ������� � �������� ����� ����
	SelectBrush(hdcEMF, hBrush);//�������� ���������


	int aX = cubeX;
	int aY = cubeY;
	int bX = aX + A;
	int bY = aY;
	int cX = bX;
	int cY = aY - A;
	int dX = aX;
	int dY = cY;
	int gX = aX;
	int gY = aY;

	int pLeft = aX - A/2, pTop = aY - A;
	int pRight = aX + A/2, pBottom = aY;
	int xStart = aX, yStart = aY - A;
	int xEnd = aX, yEnd = aY;
	
	Chord(hdcEMF, pLeft, pTop, pRight, pBottom, xStart, yStart, xEnd, yEnd);

	HBRUSH hBrush2 = CreateHatchBrush(HS_DIAGCROSS, RGB(0, 0, 139));
	SelectBrush(hdcEMF, hBrush2);

	int pLeftR = bX + A / 2, pTopR = bY - A;
	int pRightR = bX - A / 2, pBottomR = bY;
	int xStartR = bX, yStartR = bY - A;
	int xEndR = bX, yEndR = bY;

	Chord(hdcEMF, pLeftR, pTopR, pRightR, pBottomR, xEndR, yEndR, xStartR, yStartR);

	static POINT starFigure[5] = {aX, aY, bX, bY, cX, cY, dX, dY, gX, gY}; 
	HBRUSH hBrush1 = CreateHatchBrush(HS_DIAGCROSS, RGB(0, 0, 139));
	SelectBrush(hdcEMF, hBrush1);
								 
	SetPolyFillMode(hdcEMF, ALTERNATE);
	Polygon(hdcEMF, starFigure, 5);

	DeleteObject(hBrush);
	DeleteObject(hPen);
	hemf = CloseEnhMetaFile(hdcEMF);


	return TRUE;
}

BOOL OnCommand(HWND hWnd, int id, HWND hwndCtl, UINT codeNotify)
{
	wmId = LOWORD(MAKEWPARAM((UINT)(id), (UINT)(codeNotify)));

	return TRUE;
}

BOOL OnDestroy(HWND hWnd)
{
	PostQuitMessage(0);
	return TRUE;
}