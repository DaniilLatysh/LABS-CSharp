//Лабораторная работа №6 задача2
#include <Windows.h>
#include <tchar.h>
#include <Windowsx.h>
#include "resource.h"

//---Объявление функции окна или процедуры
LRESULT CALLBACK Pr2_WndProc(HWND, UINT, WPARAM, LPARAM);
//BOOL TextOut(HDC hDC, int nXStart, int nYStart, LPCTSTR lpString, int cbString);//где:
//HDC hDC дескриптор контекста устройства отображения
//int nXStart и int nYStart - координаты точки начала ввода текста
//LPCTSTR lpString - указатель на выводимую строку
//int cbString - количество выводимых символов
//BOOL WINAPI LineTo(HDC, int, int);//объявление функции для рисования линии
//Объявление функции диалогового окна
BOOL CALLBACK DlgProc(HWND hWnd, UINT message, WPARAM wParam, LPARAM lParam);
//обработчик функции OnLButtonDown
//void  OnLButtonDown(HWND hwnd, BOOL fDoubleClick, int x, int y,UINT keyFlags); 
//обработчик функции OnLButtonUp
//void  OnLButtonUp(HWND hwnd, int x, int y,UINT keyFlags); 
//обработчик функции OnMouseMove
//void  OnMouseMove(HWND hwnd, int x, int y,UINT keyFlags);
//Функция создания главного окна
//HWND Create(HINSTANCE, int);
//--------------------------------------------
//---Объявление глобальных переменных---
#define g_lpszClassName TEXT("класс пользователя")
#define g_lpszAplicationTitle TEXT("Главное окно приложения. Программист А.В.Кононенко")
HINSTANCE hInstance;
HWND hWnd_dial = NULL;

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
		WS_OVERLAPPED | WS_CAPTION | WS_SYSMENU | WS_THICKFRAME | WS_MAXIMIZEBOX | WS_MINIMIZEBOX | WS_CLIPCHILDREN,
		50, 50, 900, 600,
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
	ShowWindow(hWnd, nCmdShow);//так как окно создано только в памяти (нет параметра WS_VISIBLE) функция ShowWindow переводит его ко второму параметру nCmdShow и показывает 
	UpdateWindow(hWnd);//функция сообщает функции окна о необходимости перересовки окна

					   //---Организация цикла обработки сообщений-------
					   //&msg- указатель на структуру в которую будет записано сообщение
					   //NULL- дескриптор окна для которого нужно выбирать сообщения (в данном случае для всех окон)
					   //0 и 0 это интервал выборки в данном случае это все сообщения
	while (GetMessage(&msg, NULL, 0, 0))
	{
		TranslateMessage(&msg);//перевод вводимых с клавиатуры символов
		DispatchMessage(&msg);//распределяет сообщения по функциям окна, здесь и происходит вызов функции окна
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
		//создаем окно согласно задания
		hWnd_dial = CreateDialog((HINSTANCE)GetWindowLong(hWnd, GWL_HINSTANCE),
			MAKEINTRESOURCE(IDD_DIALOG1), hWnd, (DLGPROC)DlgProc);
		//устанавливаем необходимую позицию окна и его размеров
		SetWindowPos(hWnd_dial, 0, 90, 190, 150, 300, 0);
		//показываем окно
		ShowWindow(hWnd_dial, SW_SHOW);
		return 0;
	}
	case WM_KEYDOWN:
	{
		static RECT dl;//структура для записи прямоугольника диалогового окна
		static	RECT Wnd;//структура для записи прямоугольника родительского окна
		POINT ptClientUpL;// Клиентский верхний левый угол
		POINT ptClientLowR;// Клиентский правый нижний угол 
		GetWindowRect(hWnd_dial, &dl);//заполняем структуру
		GetWindowRect(hWnd, &Wnd);//заполняем структуру
		ptClientUpL.x = dl.left;//иниц x лев верх
		ptClientUpL.y = dl.top;//иниц y лев верх
		ptClientLowR.x = dl.right;//иниц x ниж прав
		ptClientLowR.y = dl.bottom;//иниц x ниж прав
		ScreenToClient(hWnd, &ptClientUpL);//перевод из экранных в клиентские 
		ScreenToClient(hWnd, &ptClientLowR);//перевод из экранных в клиентские 
		TCHAR buf[20];//вспомогательный буфер
		wsprintf(buf, TEXT("X = %d, Y = %d "), dl.left - Wnd.left, dl.top - Wnd.top);//записываем в буфер координаты
		HDC hdc = GetDC(hWnd);//берем контекст главного окна
		SetTextColor(hdc, RGB(255, 128, 0));//устанавливаем цвет текста
		int d = 10;
		TextOut(hdc, 30, Wnd.bottom - 120, (LPCWSTR)buf, wcslen(buf));//вывод текста

		switch (LOWORD(wParam))
		{
		case 'B'://Сдвиг диалогового окна влево
		{
			MoveWindow(hWnd_dial,
				ptClientUpL.x - d,
				ptClientUpL.y,
				ptClientLowR.x - ptClientUpL.x,
				ptClientLowR.y - ptClientUpL.y,
				TRUE);
			break;
		}
		case 'N'://Сдвиг диалогового окна вправо
		{
			MoveWindow(hWnd_dial,
				ptClientUpL.x + d,
				ptClientUpL.y,
				ptClientLowR.x - ptClientUpL.x,
				ptClientLowR.y - ptClientUpL.y,
				TRUE);
			break;
		}
		case 'M'://Сдвиг диалогового окна вверх
		{
			MoveWindow(hWnd_dial,
				ptClientUpL.x,
				ptClientUpL.y - d,
				ptClientLowR.x - ptClientUpL.x,
				ptClientLowR.y - ptClientUpL.y,
				TRUE);
			break;
		}
		case '<'://Сдвиг диалогового окна вниз
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
		return 0;
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
	HDC hdc;                       // дескриптор контекста устройства
	RECT rcClient;                 // Прямоугольник клиентской области 
	POINT ptClientUL;              // Клиентский верхний левый угол
	POINT ptClientLR;              // Клиентский правый нижний угол 
	static POINTS ptsBegin;        // Стартовая точка
	static POINTS ptsEnd;          // Новая концевая точка 
	static POINTS ptsPrevEnd;      // Предыдущая концевая точка 
	static BOOL fPrevLine = FALSE; // Предыдущий флаг линии 


	switch (message)
	{
		// Place message cases here. 

	case WM_LBUTTONDOWN:

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
		return 0;

	case WM_MOUSEMOVE:

		// Для рисования линиии при перемещении мыши нужно удерживать
		// нажатой левую кнопку мыши 
		if (wParam & MK_LBUTTON)
		{
			// Получаем контекст устройства для клиентной области 
			hdc = GetDC(hWnd);
			// Следуящая функция обеспечивает вывод пиксел 
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
			MoveToEx(hdc, ptsBegin.x, ptsBegin.y, (LPPOINT)NULL);
			LineTo(hdc, ptsEnd.x, ptsEnd.y);

			//  Устанавливае флаг предыдущей линии, сохраняем конечную точку
			//  новой линии и освобождаем  контекст устройства 
			fPrevLine = TRUE;
			ptsPrevEnd = ptsEnd;
			ReleaseDC(hWnd, hdc);
		}
		break;

	case WM_LBUTTONUP:

		// Пользователь закончил рисование линии. 
		// Сбрасываем флаг предыдущей линии,
		//  освобождаем курсор и освобождаем захват мыши 
		fPrevLine = FALSE;
		ClipCursor(NULL);
		ReleaseCapture();
		return 0;

		//////////////////////////

	default: return FALSE;

	} return FALSE;
}