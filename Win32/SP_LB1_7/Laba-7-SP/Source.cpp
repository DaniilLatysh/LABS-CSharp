#include <windows.h>
#include <windowsx.h>
#include <tchar.h>
#include <cmath>


//Прототип функции окна
LRESULT CALLBACK PR2_WndProc(HWND, UINT, WPARAM, LPARAM); // прототип оконной процедуры
														  
BOOL OnDestroy(HWND hWnd);
BOOL OnCreate(HWND hWnd, LPCREATESTRUCT lpCreateStruct);
BOOL OnPaint(HWND hWnd);
BOOL OnCommand(HWND hWnd, int id, HWND hwndCtl, UINT codeNotify);

//Глобальные переменные

HWND hWnd;
int wmId;
HDC hdc;
LPCWSTR file_name = L"D:\\MetaFile.emf";
static HDC hdcEMF;
HENHMETAFILE hemf;
ENHMETAHEADER emh;
RECT rect;
PAINTSTRUCT ps;

//  Стартовая функция
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

	// завершение работы в случае, когда регистрация класса выполнена с ошибкой
	if (!RegisterClassEx(&wc)) {
		MessageBox(NULL, TEXT("Ошибка регистрации класса окна!"), TEXT("Ошибка"), MB_OK | MB_ICONERROR);
		return FALSE;
	}

	// создание главного окна
	hWnd = CreateWindowEx(NULL,
		TEXT("SimpleClassName"),
		TEXT("Лабораторная работа 7. Программист Латыш Д.Н."),
		WS_OVERLAPPEDWINDOW,
		100, 100, // позиция окна
		700, 500, // размеры окна
		NULL, NULL, hInstance, NULL);

	// проверка результата и завершение работы в случае, когда окно не будет создано
	if (!hWnd) {
		MessageBox(NULL, TEXT("Окно не создано!"), TEXT("Ошибка"), MB_OK | MB_ICONERROR);
		return FALSE;
	}

	// отображение главного окна
	ShowWindow(hWnd, nCmdShow);
	UpdateWindow(hWnd);

	while (GetMessage(&msg, NULL, 0, 0)) {
		TranslateMessage(&msg);
		DispatchMessage(&msg);
	}

	return msg.wParam;
}

// Функция окна
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

BOOL OnPaint(HWND hWnd) //отображение текста в окне с заданными параметрами
{
	hdc = BeginPaint(hWnd, &ps); //захватим контекст
	GetClientRect(hWnd, &rect);	//клиентскую область			
	hemf = GetEnhMetaFile(L"D:\\MetaFile.emf"); //запросим метафайл
	PlayEnhMetaFile(hdc, hemf, &rect); //воспроизведем данные метафайла	
	DeleteEnhMetaFile(hemf); //удалим метафайл
	EndPaint(hWnd, &ps); //освободим контекст
	return TRUE;

}

BOOL OnCreate(HWND hWnd, LPCREATESTRUCT lpCreateStruct)
{
	hdcEMF = CreateEnhMetaFile(NULL, L"D:\\MetaFile.emf", NULL, NULL); // захватим контекст контекста устройства
	static LOGFONT lf; //создаём экземпляр LOGFONT
	WCHAR lpszFace1[] = TEXT("Microsoft Sans Serif");// название шрифта
	lf.lfCharSet = DEFAULT_CHARSET; //значение по умолчанию
	lf.lfPitchAndFamily = DEFAULT_PITCH; //значения по умолчанию
	wcscpy_s(lf.lfFaceName, lpszFace1); //копируем в строку название шрифта 
	lf.lfHeight = -MulDiv(20, GetDeviceCaps(hdcEMF, LOGPIXELSY), 72); //переводим высоту текста в реальную
	lf.lfItalic = 0; //начертание обычный
	HFONT hFont = CreateFontIndirect(&lf); //Cоздали шрифт
	SelectObject(hdcEMF, hFont); //Он будет иметь силу только когда мы его выберем
	SetTextColor(hdcEMF, RGB(127, 0, 0)); //зададим цвет текста
	SetBkMode(hdcEMF, TRANSPARENT); //прозрачная подложка
	TextOut(hdcEMF, 30, 50, TEXT("Латыш Даниил Николевич"), lstrlen(TEXT("Латыш Даниил Николевич"))); //выведем текст
	DeleteObject(hFont); //выгрузим из памяти объект шрифта
						 //вывод в окно графического изображения/
	HPEN hPen = CreatePen(PS_SOLID, 3, RGB(60, 60, 0));//перо для контура фигур сплошное, толщина 3,красное
	HBRUSH hBrush = CreateHatchBrush(HS_CROSS, RGB(0, 0, 139));//штриховка горизонтальная,синяя

	const int cubeX = 150; //начало рисования куба
	const int cubeY = 200; //начало рисования куба
	const int A = 100;//размер а
	const int B = 140;//размер в
					  //рисуем круг
	SelectPen(hdcEMF, hPen); // выберем в контекст новое перо
	SelectBrush(hdcEMF, hBrush);//выбираем штриховку


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