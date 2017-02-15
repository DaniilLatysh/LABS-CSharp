#include <windows.h>
#include <windowsx.h>
#include <tchar.h>
#include <cmath>

#define M_PI 3.14159265358979323846

//Прототип функции окна
LRESULT CALLBACK PR2_WndProc(HWND, UINT, WPARAM, LPARAM); // прототип оконной процедуры
														  //
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
int xl, yl, x2, y2;
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
		TEXT("Лабораторная работа 7. Программист А.В.Кононенко"),
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
	hemf = GetEnhMetaFile(L"D:\\emf2.emf"); //запросим метафайл
	PlayEnhMetaFile(hdc, hemf, &rect); //воспроизведем данные метафайла	
	DeleteEnhMetaFile(hemf); //удалим метафайл
	EndPaint(hWnd, &ps); //освободим контекст
	return TRUE;

}

BOOL OnCreate(HWND hWnd, LPCREATESTRUCT lpCreateStruct)
{
	hdcEMF = CreateEnhMetaFile(NULL, L"D:\\emf2.emf", NULL, NULL); // захватим контекст контекста устройства
	static LOGFONT lf; //создаём экземпляр LOGFONT
	WCHAR lpszFace1[] = TEXT("Microsoft Sans Serif");// название шрифта
	lf.lfCharSet = DEFAULT_CHARSET; //значение по умолчанию
	lf.lfPitchAndFamily = DEFAULT_PITCH; //значения по умолчанию
	wcscpy_s(lf.lfFaceName, lpszFace1); //копируем в строку название шрифта 
	lf.lfHeight = -MulDiv(20, GetDeviceCaps(hdcEMF, LOGPIXELSY), 72); //переводим высоту текста в реальную
	lf.lfItalic = 0; //начертание обычный
	HFONT hFont = CreateFontIndirect(&lf); //Cоздали шрифт
	SelectObject(hdcEMF, hFont); //Он будет иметь силу только когда мы его выберем
	SetTextColor(hdcEMF, RGB(0, 0, 139)); //зададим цвет текста
	SetBkMode(hdcEMF, TRANSPARENT); //прозрачная подложка
	TextOut(hdcEMF, 30, 50, TEXT("Латыш Даниил Николаевич"), lstrlen(TEXT("Латыш Даниил Николаевич"))); //выведем текст
	DeleteObject(hFont); //выгрузим из памяти объект шрифта
						 //вывод в окно графического изображения/
	HPEN hPen = CreatePen(PS_SOLID, 3, RGB(255, 0, 0));//перо для контура фигур сплошное, толщина 3,красное
	HBRUSH hBrush = CreateHatchBrush(HS_VERTICAL, RGB(0, 0, 139));//штриховка горизонтальная,синяя
	const int cx = 200;//центр круга
	const int cy = 200;//центр круга
	const int radius = 40;//радиус
	const int A = 100;//размер а
	const int B = 140;//размер в
					  //рисуем круг
	SelectPen(hdcEMF, hPen); // выберем в контекст новое перо
	SelectBrush(hdcEMF, hBrush);//выбираем штриховку
	Ellipse(hdcEMF, cx - radius, cy - radius, cx + radius, cy + radius);
	//DeleteObject(hBrush);//удаляем штриховку
	//определяем точки фигуры домика
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
	static POINT starFigure[5] = { bX, bY, cX, cY, dX, dY, gX, gY, fX, fY }; // определяем вершины домика
	HBRUSH hBrush1 = CreateHatchBrush(HS_HORIZONTAL, RGB(0, 0, 139));//штриховка горизонтальная,синяя
	SelectBrush(hdcEMF, hBrush1);//берем новую штриховку
								 //рисуем домик
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