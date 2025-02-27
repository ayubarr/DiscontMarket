@echo off
setlocal enabledelayedexpansion

set FLD=.\TXT
set RES=.\all.txt

set first=true

for %%a in (%FLD%\*.txt) do (
  if !first!==true (
    copy /b %%a %RES%
  ) else (
    copy /b %RES% + %%a %RES%
  )
  set first=false
)
