#include "stdafx.h"
#include "GlobalFunction.h"
// #include <Shlwapi.h>
// #pragma comment(lib,"SHLWAPI")


#define PRINT_ERROR(sz) _tprintf(_T("%s异常信息：%s,系统错误码：%d\n"),_T(sz),pError,GetLastError())

//释放DC句柄
void ReleaseDCHandle(HDC* pHDC)
{
	if(NULL!=pHDC&&NULL!=*pHDC)
	{
		::DeleteDC(*pHDC);
		*pHDC=NULL;
	}
}

//释放位图句柄
void ReleaseGDIHandle(HANDLE* pHOBJ)
{
	if(NULL!=pHOBJ&&NULL!=*pHOBJ)
	{
		::DeleteObject(*pHOBJ);
		*pHOBJ=NULL;
	}	   
}
//获取系统时间的字符串 ****-**-** **_**_**格式
std::wstring FormatSystemTimeW()
{
	std::wstring wstrRet;
	try
	{
		SYSTEMTIME sm;
		::GetLocalTime(&sm);
		wchar_t wszTime[20]={0};
		swprintf(wszTime,L"%d-%02d-%02d %02d_%02d_%02d",sm.wYear,\
			sm.wMonth,sm.wDay,sm.wHour,sm.wMinute,sm.wSecond);
		wstrRet.append(wszTime);
	}
	catch(...)
	{
		wstrRet.clear();
	}
	return wstrRet;
}

std::wstring GetAppPathW()
{
	std::wstring wstrRet;
	try
	{
	   wstrRet.resize(MAX_PATH+1);
	   if(0==::GetModuleFileNameW(NULL,(LPWCH)wstrRet.c_str(),MAX_PATH))
		   throw _T("GetModuleFileName获取模块路径失败！");
	   int nPos=wstrRet.find_last_of('\\');
	   if(std::wstring::npos==nPos)//没有找到这个字符
		   throw _T("没有找到\\，莫名错误！");
	  wstrRet= wstrRet.substr(0,nPos);
	}
	catch(TCHAR* pError)
	{
		PRINT_ERROR("GetAppPath");
		wstrRet.clear();
	}
	return wstrRet;
}
std::string GetAppPathA()
{
	std::string strRet;
	try
	{
		strRet.resize(MAX_PATH+1);
		if(0==::GetModuleFileNameA(NULL,(LPCH)strRet.c_str(),MAX_PATH))
			throw _T("GetModuleFileName获取模块路径失败！");
		int nPos=strRet.find_last_of('\\');
		if(std::string::npos==nPos)//没有找到这个字符
			throw _T("没有找到\\，莫名错误！");
		strRet=strRet.substr(0,nPos);
	}
	catch(TCHAR* pError)
	{
		PRINT_ERROR("GetAppPath");
		strRet.clear();
	}
	return strRet;
}

bool FillDCRect( IN HDC hDC,IN LPRECT lpRect,IN COLORREF color )
{
	bool bRet=false;
	try
	{
		if(NULL==hDC)
			throw  _T("DC句柄为空！");
		if(NULL==lpRect)
			throw _T("矩形区域指针为空！");
		::SetBkColor(hDC, color);
		::ExtTextOut(hDC, 0, 0, ETO_OPAQUE, lpRect, NULL, 0, NULL);
		bRet=true;
	}
	catch(TCHAR* pError)
	{
		PRINT_ERROR("FillDCRect");
	}
	return bRet;
}

HBITMAP GetBitmapRect( IN HBITMAP hBitmap,IN LPRECT lpRect )
{
	HBITMAP hRetBitmap=NULL;
	try
	{
		if((NULL==hBitmap)||(NULL==lpRect)||(IsRectEmpty(lpRect)))
			throw PARAMETER_ERROR;
		HDC hDC=CreateCompatibleDC(NULL);
		if(NULL==hDC)
			throw CREATE_COMDC_ERROR;
		HBITMAP hOldBitmap=(HBITMAP)::SelectObject(hDC,hBitmap);
		HDC hMemDC=CreateCompatibleDC(hDC);
		if(NULL==hMemDC)
			throw CREATE_COMDC_ERROR;
		int nWidth=lpRect->right-lpRect->left;
		int nHeight=lpRect->bottom-lpRect->top;
		hRetBitmap=CreateCompatibleBitmap(hDC,nWidth,nHeight);
		if(NULL==hRetBitmap)
			throw CREATE_COMBITMAP_ERROR;
		HBITMAP hOldMemBitmap=(HBITMAP)::SelectObject(hMemDC,hRetBitmap);
		::BitBlt(hMemDC,0,0,nWidth,nHeight,hDC,lpRect->left,lpRect->top,SRCCOPY);
		hRetBitmap=(HBITMAP)::SelectObject(hMemDC,hOldMemBitmap);
		::SelectObject(hDC,hOldBitmap);
		//释放GDI对象
		DeleteDC(hDC);
		DeleteDC(hMemDC);
	}
	catch(TCHAR* pError)
	{
		PRINT_ERROR("GetBitmapRect");
		hRetBitmap=NULL;
	}
	return hRetBitmap;
}

HBITMAP GetDCBackBitmap( IN HDC hDC,IN int nWidth,IN int nHeight )
{
	HBITMAP hRetBitmap=NULL;
	try
	{
		if((NULL==hDC)||(nWidth<0)||(nHeight<0))
			throw PARAMETER_ERROR;
		HDC hMemDC=CreateCompatibleDC(hDC);
		if(NULL==hMemDC)
			throw CREATE_COMDC_ERROR;
		hRetBitmap=CreateCompatibleBitmap(hDC,nWidth,nHeight);
		if(NULL==hRetBitmap)
			throw CREATE_COMDC_ERROR;
		HBITMAP hOldBitmap=(HBITMAP)::SelectObject(hMemDC,hRetBitmap);
		::BitBlt(hMemDC,0,0,nWidth,nHeight,hDC,0,0,SRCCOPY);
		hRetBitmap=(HBITMAP)::SelectObject(hMemDC,hOldBitmap);
		//释放GDI对象
		::DeleteDC(hMemDC);
	}
	catch(TCHAR* pError)
	{
		PRINT_ERROR("GetDCBackBitmap");
		hRetBitmap=NULL;
	}
	return hRetBitmap;
}

bool SaveBitmapA( IN HBITMAP hBitmap,IN const char* pSavePath )
{
	bool bResult=false;
	try
	{
		if((NULL==hBitmap)&&(NULL==pSavePath))
			throw PARAMETER_ERROR;
		BITMAP bmMetric;
		memset(&bmMetric,0,sizeof(bmMetric));
		::GetObject(hBitmap,sizeof(bmMetric),&bmMetric);
		//获取位图像素信息
		LONG bmSize=(bmMetric.bmWidth*bmMetric.bmBitsPixel+31)/8*bmMetric.bmHeight;
		BITMAPFILEHEADER fileHeader;//位图文件头
		memset(&fileHeader,0,sizeof(fileHeader));
		BITMAPINFOHEADER infoHeader;//位图信息头
		memset(&infoHeader,0,sizeof(infoHeader));
		//首先初始化位图信息头 
		infoHeader.biBitCount=32;
		infoHeader.biClrImportant=0;
		infoHeader.biClrUsed=0;
		infoHeader.biCompression=BI_RGB;
		infoHeader.biHeight=bmMetric.bmHeight;
		infoHeader.biWidth=bmMetric.bmWidth;
		infoHeader.biPlanes=1;//必须为1
		infoHeader.biSize=sizeof(BITMAPINFOHEADER);
		infoHeader.biSizeImage=0;//如果是BI_RGB，必须为0
		infoHeader.biXPelsPerMeter=0;
		infoHeader.biYPelsPerMeter=0;
		HGLOBAL hMem=::LocalAlloc(GPTR,bmSize);
		LPBITMAPINFOHEADER pData=(LPBITMAPINFOHEADER)::LocalLock(hMem);
		*pData=infoHeader;
		//::GetBitmapBits(hBitmap,bmSize,pData);
		HDC hDC=::CreateCompatibleDC(NULL);
		::GetDIBits(hDC,hBitmap,0,bmMetric.bmHeight,pData,\
			(LPBITMAPINFO)pData,DIB_RGB_COLORS);
		::DeleteObject(hDC);
		//初始化文件信息头
		fileHeader.bfOffBits=sizeof(BITMAPFILEHEADER)+sizeof(BITMAPINFOHEADER);
		fileHeader.bfSize=bmSize;
		fileHeader.bfType=0x4D42;//必须是BM
		//写入新文件
		FILE* fp=NULL;
		fp=fopen(pSavePath,"wb+");
		if(fp)
		{
			fwrite(&fileHeader,sizeof(fileHeader),1,fp);
			fwrite(&infoHeader,sizeof(infoHeader),1,fp);
			fwrite(pData,bmSize,1,fp);
			fclose(fp);
		}
		else
		{
			::LocalUnlock(hMem);
			::LocalFree(hMem);
			throw _T("创建文件失败！");
		}
		::LocalUnlock(hMem);
		::LocalFree(hMem);
		bResult=true;
	}
	catch(TCHAR* pError)
	{
		PRINT_ERROR("SaveBitmapA");
	}
	return bResult;
}

bool SaveBitmapW( IN HBITMAP hBitmap,IN const wchar_t* pSavePath )
{
	bool bResult=false;
	try
	{
		if((NULL==hBitmap)&&(NULL==pSavePath))
			throw PARAMETER_ERROR;
		BITMAP bmMetric;
		memset(&bmMetric,0,sizeof(bmMetric));
		::GetObject(hBitmap,sizeof(bmMetric),&bmMetric);
		//获取位图像素信息
		LONG bmSize=(bmMetric.bmWidth*bmMetric.bmBitsPixel+31)/8*bmMetric.bmHeight;
		BITMAPFILEHEADER fileHeader;//位图文件头
		memset(&fileHeader,0,sizeof(fileHeader));
		BITMAPINFOHEADER infoHeader;//位图信息头
		memset(&infoHeader,0,sizeof(infoHeader));
		//首先初始化位图信息头 
		infoHeader.biBitCount=32;
		infoHeader.biClrImportant=0;
		infoHeader.biClrUsed=0;
		infoHeader.biCompression=BI_RGB;
		infoHeader.biHeight=bmMetric.bmHeight;
		infoHeader.biWidth=bmMetric.bmWidth;
		infoHeader.biPlanes=1;//必须为1
		infoHeader.biSize=sizeof(BITMAPINFOHEADER);
		infoHeader.biSizeImage=0;//如果是BI_RGB，必须为0
		infoHeader.biXPelsPerMeter=0;
		infoHeader.biYPelsPerMeter=0;
		HGLOBAL hMem=::LocalAlloc(GPTR,bmSize);
		LPBITMAPINFOHEADER pData=(LPBITMAPINFOHEADER)::LocalLock(hMem);
		*pData=infoHeader;
		//::GetBitmapBits(hBitmap,bmSize,pData);
		HDC hDC=::CreateCompatibleDC(NULL);
		::GetDIBits(hDC,hBitmap,0,bmMetric.bmHeight,pData,\
			(LPBITMAPINFO)pData,DIB_RGB_COLORS);
		::DeleteObject(hDC);
		//初始化文件信息头
		fileHeader.bfOffBits=sizeof(BITMAPFILEHEADER)+sizeof(BITMAPINFOHEADER);
		fileHeader.bfSize=bmSize;
		fileHeader.bfType=0x4D42;//必须是BM
		//写入新文件
		FILE* fp=NULL;
		fp=_tfopen(pSavePath,_T("wb+"));
		if(fp)
		{
			fwrite(&fileHeader,sizeof(fileHeader),1,fp);
			fwrite(&infoHeader,sizeof(infoHeader),1,fp);
			fwrite(pData,bmSize,1,fp);
			fclose(fp);
		}
		else
		{
			::LocalUnlock(hMem);
			::LocalFree(hMem);
			throw _T("创建文件失败！");
		}
		::LocalUnlock(hMem);
		::LocalFree(hMem);
		bResult=true;
	}
	catch(TCHAR* pError)
	{
		PRINT_ERROR("SaveBitmapW");
	}
	return bResult;
}

HBITMAP CopyBitmap( IN HBITMAP hBitmap )
{
	HBITMAP hRetBitmap=NULL;
	try
	{
		if(NULL==hBitmap)
			throw PARAMETER_ERROR;
		BITMAP bmMetric={0};
		if(0==GetObject(hBitmap,sizeof(bmMetric),&bmMetric))
			throw GETOBJECT_ERROR;
		HDC hDC=CreateCompatibleDC(NULL);
		HBITMAP hOldBitmap=(HBITMAP)::SelectObject(hDC,hBitmap);
		HDC hMemDC=CreateCompatibleDC(hDC);
		hRetBitmap=CreateCompatibleBitmap(hDC,bmMetric.bmWidth,bmMetric.bmHeight);
		HBITMAP hOldMemBitmap=(HBITMAP)SelectObject(hMemDC,hRetBitmap);
		::BitBlt(hMemDC,0,0,bmMetric.bmWidth,bmMetric.bmHeight,hDC,0,0,SRCCOPY);
		::SelectObject(hDC,hOldBitmap);
		hRetBitmap=(HBITMAP)SelectObject(hMemDC,hOldMemBitmap);
		::DeleteDC(hDC);
		::DeleteDC(hMemDC);
	}
	catch(TCHAR* pError)
	{
		PRINT_ERROR("CopyBitmap");
	}
	return hRetBitmap;
}