#include <windows.h>
#include <windowsx.h>
#include <tchar.h>
#include <cmath>

#define M_PI 3.14159265358979323846

//�������� ������� ����
LRESULT CALLBACK PR2_WndProc(HWND, UINT, WPARAM, LPARAM); // �������� ������� ���������
														  //
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
int xl, yl, x2, y2;
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
		TEXT("������������ ������ 7. ����������� �.�.���������"),
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
	hemf = GetEnhMetaFile(L"D:\\emf2.emf"); //�������� ��������
	PlayEnhMetaFile(hdc, hemf, &rect); //������������� ������ ���������	
	DeleteEnhMetaFile(hemf); //������ ��������
	EndPaint(hWnd, &ps); //��������� ��������
	return TRUE;

}

BOOL OnCreate(HWND hWnd, LPCREATESTRUCT lpCreateStruct)
{
	hdcEMF = CreateEnhMetaFile(NULL, L"D:\\emf2.emf", NULL, NULL); // �������� �������� ��������� ����������
	static LOGFONT lf; //������ ��������� LOGFONT
	WCHAR lpszFace1[] = TEXT("Microsoft Sans Serif");// �������� ������
	lf.lfCharSet = DEFAULT_CHARSET; //�������� �� ���������
	lf.lfPitchAndFamily = DEFAULT_PITCH; //�������� �� ���������
	wcscpy_s(lf.lfFaceName, lpszFace1); //�������� � ������ �������� ������ 
	lf.lfHeight = -MulDiv(20, GetDeviceCaps(hdcEMF, LOGPIXELSY), 72); //��������� ������ ������ � ��������
	lf.lfItalic = 0; //���������� �������
	HFONT hFont = CreateFontIndirect(&lf); //C������ �����
	SelectObject(hdcEMF, hFont); //�� ����� ����� ���� ������ ����� �� ��� �������
	SetTextColor(hdcEMF, RGB(0, 0, 139)); //������� ���� ������
	SetBkMode(hdcEMF, TRANSPARENT); //���������� ��������
	TextOut(hdcEMF, 30, 50, TEXT("����� ������ ����������"), lstrlen(TEXT("����� ������ ����������"))); //������� �����
	DeleteObject(hFont); //�������� �� ������ ������ ������
						 //����� � ���� ������������ �����������/
	HPEN hPen = CreatePen(PS_SOLID, 3, RGB(255, 0, 0));//���� ��� ������� ����� ��������, ������� 3,�������
	HBRUSH hBrush = CreateHatchBrush(HS_VERTICAL, RGB(0, 0, 139));//��������� ��������������,�����
	const int cx = 200;//����� �����
	const int cy = 200;//����� �����
	const int radius = 40;//������
	const int A = 100;//������ �
	const int B = 140;//������ �
					  //������ ����
	SelectPen(hdcEMF, hPen); // ������� � �������� ����� ����
	SelectBrush(hdcEMF, hBrush);//�������� ���������
	Ellipse(hdcEMF, cx - radius, cy - radius, cx + radius, cy + radius);
	//DeleteObject(hBrush);//������� ���������
	//���������� ����� ������ ������
	int aX = cx + radius;
	int aY = cy;
	int bX = aX;
	int bY = aY - A / 2;
	int cX = bX + B / 2;
	int cY = bY - A / 2;
	int dX = bX + B;
	int dY = bY;
	int gX = dX;
	int gY = dY + A;
	int fX = aX;
	int fY = gY;
	static POINT starFigure[5] = { bX, bY, cX, cY, dX, dY, gX, gY, fX, fY }; // ���������� ������� ������
	HBRUSH hBrush1 = CreateHatchBrush(HS_HORIZONTAL, RGB(0, 0, 139));//��������� ��������������,�����
	SelectBrush(hdcEMF, hBrush1);//����� ����� ���������
								 //������ �����
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