	// LATYSH.cpp : Defines the entry point for the application.
	//

	#include "stdafx.h"
	#include <stdio.h>
	#include <stdlib.h>
	#include <conio.h>
	#include <process.h>   
	#include <locale.h>     
	#include <string.h>
	#include <ctype.h>
	#include <windows.h>
	#include "stdafx.h"
	#include "resource.h"
	#include "LATYSH.h"

	#define MAX_LOADSTRING 100
	#define FILENAME L"text.txt"

	// Global Variables:
	HINSTANCE hInst;								// current instance
	TCHAR szTitle[MAX_LOADSTRING];					// The title bar text
	TCHAR szWindowClass[MAX_LOADSTRING];			// the main window class name
	static HMENU hMenu;



	// Forward declarations of functions included in this code module:
	ATOM				MyRegisterClass(HINSTANCE hInstance);
	BOOL				InitInstance(HINSTANCE, int);
	LRESULT CALLBACK	WndProc(HWND, UINT, WPARAM, LPARAM);
	INT_PTR CALLBACK	About(HWND, UINT, WPARAM, LPARAM);

	int APIENTRY _tWinMain(HINSTANCE hInstance,
		HINSTANCE hPrevInstance,
		LPTSTR    lpCmdLine,
		int       nCmdShow)
	{
		UNREFERENCED_PARAMETER(hPrevInstance);
		UNREFERENCED_PARAMETER(lpCmdLine);


		// TODO: Place code here.
		MSG msg;
		HACCEL hAccelTable;

		// Initialize global strings
		LoadString(hInstance, IDS_APP_TITLE, szTitle, MAX_LOADSTRING);
		LoadString(hInstance, IDC_LATYSH, szWindowClass, MAX_LOADSTRING);
		MyRegisterClass(hInstance);

		// Perform application initialization:
		if (!InitInstance(hInstance, nCmdShow))
		{
			return FALSE;
		}

		hAccelTable = LoadAccelerators(hInstance, MAKEINTRESOURCE(IDC_LATYSH));

		// Main message loop:
		while (GetMessage(&msg, NULL, 0, 0))
		{
			if (!TranslateAccelerator(msg.hwnd, hAccelTable, &msg))
			{
				TranslateMessage(&msg);
				DispatchMessage(&msg);
			}
		}

		return (int)msg.wParam;
	}



	//
	//  FUNCTION: MyRegisterClass()
	//
	//  PURPOSE: Registers the window class.
	//
	//  COMMENTS:
	//
	//    This function and its usage are only necessary if you want this code
	//    to be compatible with Win32 systems prior to the 'RegisterClassEx'
	//    function that was added to Windows 95. It is important to call this function
	//    so that the application will get 'well formed' small icons associated
	//    with it.
	//
	ATOM MyRegisterClass(HINSTANCE hInstance)
	{
		WNDCLASSEX wcex;

		wcex.cbSize = sizeof(WNDCLASSEX);

		wcex.style = CS_HREDRAW | CS_VREDRAW;
		wcex.lpfnWndProc = WndProc;
		wcex.cbClsExtra = 0;
		wcex.cbWndExtra = 0;
		wcex.hInstance = hInstance;
		wcex.hIcon = LoadIcon(hInstance, MAKEINTRESOURCE(IDI_LATYSH));
		wcex.hCursor = LoadCursor(NULL, IDC_ARROW);
		wcex.hbrBackground = (HBRUSH)(COLOR_WINDOW + 1);
		wcex.lpszMenuName = MAKEINTRESOURCE(IDC_LATYSH);
		wcex.lpszClassName = szWindowClass;
		wcex.hIconSm = LoadIcon(wcex.hInstance, MAKEINTRESOURCE(IDI_SMALL));

		return RegisterClassEx(&wcex);

	}

	//
	//   FUNCTION: InitInstance(HINSTANCE, int)
	//
	//   PURPOSE: Saves instance handle and creates main window
	//
	//   COMMENTS:
	//
	//        In this function, we save the instance handle in a global variable and
	//        create and display the main program window.
	//
	BOOL InitInstance(HINSTANCE hInstance, int nCmdShow)
	{
		HWND hWnd;
		hInst = hInstance; // Store instance handle in our global variable
		hWnd = CreateWindow(szWindowClass, szTitle, WS_OVERLAPPEDWINDOW,
			CW_USEDEFAULT, 0, CW_USEDEFAULT, 0, NULL, NULL, hInstance, NULL);

		if (!hWnd)
		{
			return FALSE;
		}
		hMenu = GetMenu(hWnd);
		EnableMenuItem(hMenu, IDM_OPEN_FILE, MF_ENABLED);
		EnableMenuItem(hMenu, IDM_OBJECT_FILE_PROJECT, MF_GRAYED);
		EnableMenuItem(hMenu, IDM_CREATE_WINDOW_PROJECT, MF_GRAYED);
		EnableMenuItem(hMenu, IDM_CLOSE_WINDOW_PROJECT, MF_GRAYED);
		EnableMenuItem(hMenu, IDM_CLOSE_FILE, MF_GRAYED);
		EnableMenuItem(hMenu, IDM_VIEW_FILE, MF_GRAYED);
		ShowWindow(hWnd, nCmdShow);
		UpdateWindow(hWnd);

		return TRUE;
	}

	//
	//  FUNCTION: WndProc(HWND, UINT, WPARAM, LPARAM)
	//
	//  PURPOSE:  Processes messages for the main window.
	//
	//  WM_COMMAND	- process the application menu
	//  WM_PAINT	- Paint the main window
	//  WM_DESTROY	- post a quit message and return
	//
	//
	LRESULT CALLBACK WndProc(HWND hWnd, UINT message, WPARAM wParam, LPARAM lParam)
	{
		int wmId, wmEvent;
		PAINTSTRUCT ps;
		HDC hdc;
		static HANDLE hFile; //Для дескриптора объекта файл
		static HANDLE hFileMap; //Для дескриптора объекта "проецируемый файл"
		STARTUPINFO si = { 0 }; //Структура для функции
		PROCESS_INFORMATION pi; //Структура для создания процесса
		char * addBuffer = new char[75];
		LPSTR  lpchANSI; //Указатель на ANSI строку
		static LPVOID lpvFile; //Для адреса региона в адресном пространстве
							   //куда мы отобразим файл
		static DWORD dwFileSize; //Для значения размера файла
		int count = 0;//счетчик слов с нужной длиной
		char*tokenPtr;//указатель на лексему в строке
					  //в ANSI или UNICODE
		switch (message)
		{
		case WM_COMMAND:
			wmId = LOWORD(wParam);
			wmEvent = HIWORD(wParam);
			// Parse the menu selections:
			switch (wmId)
			{
			case IDM_OPEN_FILE:
			{
				// Открываем файл для чтения и записи. Для этого создаем объект
				// ядра "Файл". В зависимости от указанных параметров функция 
				// CreateFile либо открывает существующий файл, либо создает новый.
				// Только эта функция может использоваться для открытия файла,
				// проецируемого в память. Функция возвращает дескриптор открытого
				// объекта ядра, или код ошибки INVALID_HANDLE_VALUE.
				hFile = CreateFile(
					FILENAME, //указатель на строку с именем файла
					GENERIC_WRITE | GENERIC_READ, //требуемый уровень доступа
					0, //режим разделения, 0 - безраздельный доступ к файлу
					NULL, // LPSECURITY_ATTRIBUTES=0 - объект не наследуемый.
					OPEN_EXISTING, //Если файл не существует, то возвратить ошибку.
					FILE_ATTRIBUTE_NORMAL, //Оставить атрибуты как есть
					NULL //Не давать имя объекту ядра "Файл"
				);
				//проверяем результат
				if (hFile == INVALID_HANDLE_VALUE)
				{
					//Открыть файл не удалось
					MessageBox(NULL, L"Открыть файл не удалось", L"Ошибка", MB_OK);
					return (0);
				}
				EnableMenuItem(hMenu, IDM_OPEN_FILE, MF_GRAYED);
				EnableMenuItem(hMenu, IDM_OBJECT_FILE_PROJECT, MF_ENABLED);
			}
			break;
			case IDM_OBJECT_FILE_PROJECT:
			{
				// Узнаем размер файла. Второй параметр NULL, так как предполагается,
				//	что файл меньше 4 Гб.
				dwFileSize = GetFileSize(hFile, NULL);
				// Создаем объект "проецируемый файл". Он - на 1 байт больше, чем
				// размер файла, чтобы можно было записать в конец файла нулевой 
				// символ  и работать с файлом как с нуль-терминированной строкой.
				// Поскольку пока еще неизвестно содержит файл ANSI - или Unicode
				// - символы, и добавляя строку \nСлов с количеством символов <=4 в тексте %d
				// увеличиваем размер файла на 50*WCHAR
				hFileMap = CreateFileMapping(
					hFile, // Дескриптор проецирумого объекта "Файл" 
					NULL,
					PAGE_READWRITE, // Атрибуты защиты страниц 
					0, // LPSECURITY_ATTRIBUTES
					dwFileSize + sizeof(WCHAR), //Младшие 32 разряда
					NULL //Старшие 32 разряда размера файла
				);
				//проверка
				if (hFileMap == NULL)
				{
					// Открыть объект "проецируемый файл" не удалось
					MessageBox(NULL, L"Открыть проецируемый файл не удалось", L"Ошибка", MB_OK);
					CloseHandle(hFile);// Перед выходом закрываем открытые объекты
					return (0);
				}
				EnableMenuItem(hMenu, IDM_OBJECT_FILE_PROJECT, MF_GRAYED);
				EnableMenuItem(hMenu, IDM_CREATE_WINDOW_PROJECT, MF_ENABLED);

			}
			break;
			case IDM_CREATE_WINDOW_PROJECT:
			{
				// Открываем отображение файла на виртуальное адресное пространство и
				// получаем адрес, начиная с которого располагается образ файла в памяти.
				lpvFile = MapViewOfFile(
					hFileMap, // Дескриптор объекта "Проецируемый файл"
					FILE_MAP_WRITE, // Режим доступа
					0, // Старшие 32 разряда смещения от начала файла, 
					0, // младшие 32 разряда смещения от начала файла
					0  // и количество отображаемых байт. 0 - весь файл.
				);
				if (lpvFile == NULL)
				{
					// Спроецировать оконное представление файла не удалось
					MessageBox(NULL, L"Спроецировать оконное представление файла не удалось", L"Ошибка", MB_OK);
					CloseHandle(hFileMap);// Перед выходом закрываем открытые объекты
					CloseHandle(hFile);
					return(0);
				}
				// Записываем в конец файла нулевой символ
				lpchANSI = (LPSTR)lpvFile;
				//lpchANSI[dwFileSize] = 0;
				//char buff[1000]={0};//буфер для приема строки, считанной из файла
				//char add_buff[200]={0};//вспомогательный буфер
				//разбиваем строку на лексемы и возвращаем адрес первой лексемы
				tokenPtr = strtok(lpchANSI, "!?,.:- ");
				while (tokenPtr != NULL)
				{
					if (strlen(tokenPtr) <= 4)
					{
						count++;
					}
					tokenPtr = strtok(NULL, "!?,.:- ");
				}
				sprintf(addBuffer, "\r\nСлов с количеством символов <=4 в тексте %d", count);
				lstrcpy((LPWSTR)&lpchANSI[dwFileSize], (LPWSTR)addBuffer);
				dwFileSize += lstrlen((LPTSTR)addBuffer);
				lpchANSI[dwFileSize] = 0;
				EnableMenuItem(hMenu, IDM_CREATE_WINDOW_PROJECT, MF_GRAYED);
				EnableMenuItem(hMenu, IDM_CLOSE_WINDOW_PROJECT, MF_ENABLED);
			}
			break;
			case IDM_CLOSE_WINDOW_PROJECT:
			{
				// Очищаем все перед завершением
				// Закрываем представление файла в окне адресного пространства
				UnmapViewOfFile(lpvFile);
				// Уменьшаем счетчик ссылок на объект ядра "Проецируемый файл"
				CloseHandle(hFileMap);
				EnableMenuItem(hMenu, IDM_CLOSE_WINDOW_PROJECT, MF_GRAYED);
				EnableMenuItem(hMenu, IDM_CLOSE_FILE, MF_ENABLED);
			}
			break;
			case IDM_CLOSE_FILE:
			{
				// Удаляем добавленный ранее концевой нулевой байт.Для этого
				// перемещаем указатель файла в конец на нулевой байт,
				// а затем даем команду установить в этом месте конец файла
				SetFilePointer(hFile, dwFileSize, NULL, FILE_BEGIN);
				SetEndOfFile(hFile);
				CloseHandle(hFile);
				EnableMenuItem(hMenu, IDM_CLOSE_FILE, MF_GRAYED);
				EnableMenuItem(hMenu, IDM_VIEW_FILE, MF_ENABLED);
			}
			break;
			case IDM_VIEW_FILE:
			{
				// Запускаем NOTEPAD и загружаем в него созданный файл,
				// чтобы увидеть плоды своих трудов
				si.cb = sizeof(si);// Заполняем поле размера структуры si
				si.wShowWindow = SW_SHOW;// Задаем режим показа окна NOTEPAD
				si.dwFlags = STARTF_USESHOWWINDOW;// Устанавливаем флаг - учитывать
												  // значение поля wShowWindow
				CreateProcess(TEXT("C:\\WINDOWS\\system32\\notepad.exe"),
					TEXT(" D:\\text.txt"),
					0, 0, FALSE, 0, NULL, NULL, &si, &pi);
				{
					// Если процесс создан, освобождаем 
					// дескрипторы потока и процесса	
					CloseHandle(pi.hThread);
					CloseHandle(pi.hProcess);
					EnableMenuItem(hMenu, IDM_VIEW_FILE, MF_GRAYED);
					EnableMenuItem(hMenu, IDM_OPEN_FILE, MF_ENABLED);
				}
				return(0);
			}
			break;
			case IDM_ABOUT:
				DialogBox(hInst, MAKEINTRESOURCE(IDD_ABOUTBOX), hWnd, About);
				break;
			case IDM_EXIT:
				DestroyWindow(hWnd);
				break;
			default:
				return DefWindowProc(hWnd, message, wParam, lParam);
			}
			break;
		case WM_PAINT:
			hdc = BeginPaint(hWnd, &ps);
			// TODO: Add any drawing code here...
			EndPaint(hWnd, &ps);
			break;
		case WM_DESTROY:
			PostQuitMessage(0);
			break;
		default:
			return DefWindowProc(hWnd, message, wParam, lParam);
		}
		return 0;
	}

	// Message handler for about box.
	INT_PTR CALLBACK About(HWND hDlg, UINT message, WPARAM wParam, LPARAM lParam)
	{
		UNREFERENCED_PARAMETER(lParam);
		switch (message)
		{
		case WM_INITDIALOG:
			return (INT_PTR)TRUE;

		case WM_COMMAND:
			if (LOWORD(wParam) == IDOK || LOWORD(wParam) == IDCANCEL)
			{
				EndDialog(hDlg, LOWORD(wParam));
				return (INT_PTR)TRUE;
			}
			break;
		}
		return (INT_PTR)FALSE;
	}

