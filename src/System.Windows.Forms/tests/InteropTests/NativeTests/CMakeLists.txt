cmake_minimum_required (VERSION 2.8.12)
project (NativeTests)
set(CMAKE_MACOSX_RPATH 1)
set(CMAKE_CXX_STANDARD 11)
set(CMAKE_CXX_STANDARD_REQUIRED ON)

add_library(NativeTests SHARED
    AccessibleObjectTests.cpp
    WebBrowserSiteBaseInteropTests.cpp
)
install(TARGETS NativeTests)
