// The following ifdef block is the standard way of creating macros which make exporting 
// from a DLL simpler. All files within this DLL are compiled with the RGB_EXPORTS
// symbol defined on the command line. This symbol should not be defined on any project
// that uses this DLL. This way any other project whose source files include this file see 
// RGB_API functions as being imported from a DLL, whereas this DLL sees symbols
// defined with this macro as being exported.
#ifdef RGB_EXPORTS
#define RGB_API __declspec(dllexport)
#else
#define RGB_API __declspec(dllimport)
#endif

// This class is exported from the RGB.dll
class RGB_API CRGB {
public:
	CRGB(void);
	// TODO: add your methods here.
};

extern RGB_API int nRGB;

RGB_API int fnRGB(void);


extern RGB_API int R;
extern RGB_API int G;
extern RGB_API int B;