/*
*                       Лабораторная работа: 6 задача 2
*
*/

#include <Windows.h>
#include <tchar.h>
#include <Windowsx.h>
#include "resource.h"

//---Объявление функции окна или процедуры
LRESULT CALLBACK Pr2_WndProc(HWND, UINT, WPARAM, LPARAM);

//---Объявление глобальных переменных---
#define g_lpszClassName TEXT("Пользовательский класс")
#define g_lpszAplicationTitle TEXT("Главное окно приложения. Программист Латыш Д.Н.")
HINSTANCE hInstance;

//------Стартовая функция-----------
int APIENTRY _tWinMain(
	HINSTANCE hInstance,
	HINSTANCE hPrevInstance,
	LPTSTR lpszCmdLine,
	int nCmdShow)
{
	WNDCLASSEX wc;
	MSG msg;
	HWND hWnd;
	//---Заполняем поля структуры----
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
	//----Проверка регистрации класса окна---------
	if (!RegisterClassEx(&wc))
	{
		MessageBox(NULL, TEXT("Ошибка регистрации класса окна"), TEXT("Ошибка"), MB_OK | MB_ICONERROR);
		return FALSE;
	}
	//-----Создание окна--------
	hWnd = CreateWindowEx(NULL,
		g_lpszClassName,
		g_lpszAplicationTitle,
		WS_OVERLAPPED | WS_CAPTION | WS_SYSMENU | WS_THICKFRAME | WS_MAXIMIZEBOX | WS_MINIMIZEBOX,
		200, 200, 800, 300,
		NULL,
		NULL,
		hInstance,
		NULL);
	//---Проверка создания окна----
	if (!hWnd)
	{
		MessageBox(NULL, TEXT("Окно не создано!"), TEXT("Ошибка"), MB_OK | MB_ICONERROR);
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
//------Функция окна или процедура окна------
LRESULT CALLBACK Pr2_WndProc(HWND hWnd, UINT msg, WPARAM wParam, LPARAM lParam)
{
	TCHAR BUFF[250];//вспомогательный буфер
	HDC hdc;                       // дескриптор контекста устройства
	RECT rcClient;                 // Прямоугольник клиентской области 
	POINT ptClientUL;              // Клиентский верхний левый угол
	POINT ptClientLR;              // Клиентский правый нижний угол 
	static POINTS ptsBegin;        // Стартовая точка
	static POINTS ptsEnd;          // Новая концевая точка 
	static POINTS ptsPrevEnd;      // Предыдущая концевая точка 
	static BOOL fPrevLine = FALSE; // Предыдущий флаг линии 

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
		// Захват ввода мыши 
		SetCapture(hWnd);

		// Получение аппаратных координат дисплея для клиентской области,
		// и преобразование их в клиентские координаты.. 
		GetClientRect(hWnd, &rcClient);
		ptClientUL.x = rcClient.left;
		ptClientUL.y = rcClient.top;

		// Добавляем по единице к правой и нижней сторонам,
		// потому что координаты, полученные через  GetClientRect 
		// не включают самый левый и самый нижний пикселы.

		ptClientLR.x = rcClient.right + 1;
		ptClientLR.y = rcClient.bottom + 1;
		ClientToScreen(hWnd, &ptClientUL);
		ClientToScreen(hWnd, &ptClientLR);

		// Копируем клиентные координаты клиентной области в структуру rcClient 
		// Передавая  структуру rcClient  функции ClipCursor ограничим 
		// перемешения курсора мыши клиентской областью.
		SetRect(&rcClient, ptClientUL.x, ptClientUL.y,
			ptClientLR.x, ptClientLR.y);
		ClipCursor(&rcClient);

		// Конвертируем координаты курсора в структуру POINTS, 
		// которая задает стартовую точку линии, 
		// выводимой при обработке сообщения WM_MOUSEMOVE. 
		ptsBegin = MAKEPOINTS(lParam);
		SetWindowText(GetDlgItem(hWnd, 1001), TEXT("\0"));//обнуляем edit
		memset(BUFF, 0, sizeof(&BUFF));//обнуляем буфер
		wsprintf(BUFF, TEXT("Координаты мыши: X=%d : Y=%d"), ptsBegin.x, ptsBegin.y);
		SendMessage(GetDlgItem(hWnd, 1001), EM_REPLACESEL, 0, (LPARAM)(BUFF));
		return 0;
	}
	case WM_MOUSEMOVE:
	{
		// Для рисования линиии при перемещении мыши нужно удерживать
		// нажатой левую кнопку мыши 
		if (wParam & MK_LBUTTON)
		{
			// Получаем контекст устройства для клиентной области 
			hdc = GetDC(hWnd);
			// Следующая функция обеспечивает вывод пиксел 
			//  ранее нарисованной линии белым цветом, а пиксел новой линии черным 
			SetROP2(hdc, R2_NOTXORPEN);

			// Если вывод идет по линии, нарисованной при обработке предыдущего
			// сообщения  WM_MOUSEMOVE, то рисование осуществляется поверх. 
			// Установка цвета к белому приводит к стиранию линии 
			if (fPrevLine)
			{
				MoveToEx(hdc, ptsBegin.x, ptsBegin.y,
					(LPPOINT)NULL);
				LineTo(hdc, ptsPrevEnd.x, ptsPrevEnd.y);
			}

			// Пеобразование текущих координат курсора к структуре 
			// POINTS  и рисование новой линии. 

			ptsEnd = MAKEPOINTS(lParam);
			SetWindowText(GetDlgItem(hWnd, 1001), TEXT("\0"));//обнуляем edit
			memset(BUFF, 0, sizeof(&BUFF));//обнуляем буфер
			wsprintf(BUFF, TEXT("Координаты мыши: X=%d : Y=%d"), ptsEnd.x, ptsEnd.y);
			SendMessage(GetDlgItem(hWnd, 1001), EM_REPLACESEL, 0, (LPARAM)(BUFF));
			MoveToEx(hdc, ptsBegin.x, ptsBegin.y, (LPPOINT)NULL);
			LineTo(hdc, ptsEnd.x, ptsEnd.y);

			//  Устанавливаем флаг предыдущей линии, сохраняем конечную точку
			//  новой линии и освобождаем  контекст устройства 
			fPrevLine = TRUE;
			ptsPrevEnd = ptsEnd;
			ReleaseDC(hWnd, hdc);
		}
		break;
	}
	case WM_LBUTTONUP:
	{
		// Пользователь закончил рисование линии. 
		// Сбрасываем флаг предыдущей линии,
		//  освобождаем курсор и освобождаем захват мыши 
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
