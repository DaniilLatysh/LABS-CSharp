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
		static HANDLE hFile; //��� ����������� ������� ����
		static HANDLE hFileMap; //��� ����������� ������� "������������ ����"
		STARTUPINFO si = { 0 }; //��������� ��� �������
		PROCESS_INFORMATION pi; //��������� ��� �������� ��������
		char * addBuffer = new char[75];
		LPSTR  lpchANSI; //��������� �� ANSI ������
		static LPVOID lpvFile; //��� ������ ������� � �������� ������������
							   //���� �� ��������� ����
		static DWORD dwFileSize; //��� �������� ������� �����
		int count = 0;//������� ���� � ������ ������
		char*tokenPtr;//��������� �� ������� � ������
					  //� ANSI ��� UNICODE
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
				// ��������� ���� ��� ������ � ������. ��� ����� ������� ������
				// ���� "����". � ����������� �� ��������� ���������� ������� 
				// CreateFile ���� ��������� ������������ ����, ���� ������� �����.
				// ������ ��� ������� ����� �������������� ��� �������� �����,
				// ������������� � ������. ������� ���������� ���������� ���������
				// ������� ����, ��� ��� ������ INVALID_HANDLE_VALUE.
				hFile = CreateFile(
					FILENAME, //��������� �� ������ � ������ �����
					GENERIC_WRITE | GENERIC_READ, //��������� ������� �������
					0, //����� ����������, 0 - ������������� ������ � �����
					NULL, // LPSECURITY_ATTRIBUTES=0 - ������ �� �����������.
					OPEN_EXISTING, //���� ���� �� ����������, �� ���������� ������.
					FILE_ATTRIBUTE_NORMAL, //�������� �������� ��� ����
					NULL //�� ������ ��� ������� ���� "����"
				);
				//��������� ���������
				if (hFile == INVALID_HANDLE_VALUE)
				{
					//������� ���� �� �������
					MessageBox(NULL, L"������� ���� �� �������", L"������", MB_OK);
					return (0);
				}
				EnableMenuItem(hMenu, IDM_OPEN_FILE, MF_GRAYED);
				EnableMenuItem(hMenu, IDM_OBJECT_FILE_PROJECT, MF_ENABLED);
			}
			break;
			case IDM_OBJECT_FILE_PROJECT:
			{
				// ������ ������ �����. ������ �������� NULL, ��� ��� ��������������,
				//	��� ���� ������ 4 ��.
				dwFileSize = GetFileSize(hFile, NULL);
				// ������� ������ "������������ ����". �� - �� 1 ���� ������, ���
				// ������ �����, ����� ����� ���� �������� � ����� ����� ������� 
				// ������  � �������� � ������ ��� � ����-��������������� �������.
				// ��������� ���� ��� ���������� �������� ���� ANSI - ��� Unicode
				// - �������, � �������� ������ \n���� � ����������� �������� <=4 � ������ %d
				// ����������� ������ ����� �� 50*WCHAR
				hFileMap = CreateFileMapping(
					hFile, // ���������� ������������ ������� "����" 
					NULL,
					PAGE_READWRITE, // �������� ������ ������� 
					0, // LPSECURITY_ATTRIBUTES
					dwFileSize + sizeof(WCHAR), //������� 32 �������
					NULL //������� 32 ������� ������� �����
				);
				//��������
				if (hFileMap == NULL)
				{
					// ������� ������ "������������ ����" �� �������
					MessageBox(NULL, L"������� ������������ ���� �� �������", L"������", MB_OK);
					CloseHandle(hFile);// ����� ������� ��������� �������� �������
					return (0);
				}
				EnableMenuItem(hMenu, IDM_OBJECT_FILE_PROJECT, MF_GRAYED);
				EnableMenuItem(hMenu, IDM_CREATE_WINDOW_PROJECT, MF_ENABLED);

			}
			break;
			case IDM_CREATE_WINDOW_PROJECT:
			{
				// ��������� ����������� ����� �� ����������� �������� ������������ �
				// �������� �����, ������� � �������� ������������� ����� ����� � ������.
				lpvFile = MapViewOfFile(
					hFileMap, // ���������� ������� "������������ ����"
					FILE_MAP_WRITE, // ����� �������
					0, // ������� 32 ������� �������� �� ������ �����, 
					0, // ������� 32 ������� �������� �� ������ �����
					0  // � ���������� ������������ ����. 0 - ���� ����.
				);
				if (lpvFile == NULL)
				{
					// ������������� ������� ������������� ����� �� �������
					MessageBox(NULL, L"������������� ������� ������������� ����� �� �������", L"������", MB_OK);
					CloseHandle(hFileMap);// ����� ������� ��������� �������� �������
					CloseHandle(hFile);
					return(0);
				}
				// ���������� � ����� ����� ������� ������
				lpchANSI = (LPSTR)lpvFile;
				//lpchANSI[dwFileSize] = 0;
				//char buff[1000]={0};//����� ��� ������ ������, ��������� �� �����
				//char add_buff[200]={0};//��������������� �����
				//��������� ������ �� ������� � ���������� ����� ������ �������
				tokenPtr = strtok(lpchANSI, "!?,.:- ");
				while (tokenPtr != NULL)
				{
					if (strlen(tokenPtr) <= 4)
					{
						count++;
					}
					tokenPtr = strtok(NULL, "!?,.:- ");
				}
				sprintf(addBuffer, "\r\n���� � ����������� �������� <=4 � ������ %d", count);
				lstrcpy((LPWSTR)&lpchANSI[dwFileSize], (LPWSTR)addBuffer);
				dwFileSize += lstrlen((LPTSTR)addBuffer);
				lpchANSI[dwFileSize] = 0;
				EnableMenuItem(hMenu, IDM_CREATE_WINDOW_PROJECT, MF_GRAYED);
				EnableMenuItem(hMenu, IDM_CLOSE_WINDOW_PROJECT, MF_ENABLED);
			}
			break;
			case IDM_CLOSE_WINDOW_PROJECT:
			{
				// ������� ��� ����� �����������
				// ��������� ������������� ����� � ���� ��������� ������������
				UnmapViewOfFile(lpvFile);
				// ��������� ������� ������ �� ������ ���� "������������ ����"
				CloseHandle(hFileMap);
				EnableMenuItem(hMenu, IDM_CLOSE_WINDOW_PROJECT, MF_GRAYED);
				EnableMenuItem(hMenu, IDM_CLOSE_FILE, MF_ENABLED);
			}
			break;
			case IDM_CLOSE_FILE:
			{
				// ������� ����������� ����� �������� ������� ����.��� �����
				// ���������� ��������� ����� � ����� �� ������� ����,
				// � ����� ���� ������� ���������� � ���� ����� ����� �����
				SetFilePointer(hFile, dwFileSize, NULL, FILE_BEGIN);
				SetEndOfFile(hFile);
				CloseHandle(hFile);
				EnableMenuItem(hMenu, IDM_CLOSE_FILE, MF_GRAYED);
				EnableMenuItem(hMenu, IDM_VIEW_FILE, MF_ENABLED);
			}
			break;
			case IDM_VIEW_FILE:
			{
				// ��������� NOTEPAD � ��������� � ���� ��������� ����,
				// ����� ������� ����� ����� ������
				si.cb = sizeof(si);// ��������� ���� ������� ��������� si
				si.wShowWindow = SW_SHOW;// ������ ����� ������ ���� NOTEPAD
				si.dwFlags = STARTF_USESHOWWINDOW;// ������������� ���� - ���������
												  // �������� ���� wShowWindow
				CreateProcess(TEXT("C:\\WINDOWS\\system32\\notepad.exe"),
					TEXT(" D:\\text.txt"),
					0, 0, FALSE, 0, NULL, NULL, &si, &pi);
				{
					// ���� ������� ������, ����������� 
					// ����������� ������ � ��������	
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

