find src/* -name "bin" -type d -exec rm -rf {} \;
find src/* -name "obj" -type d -exec rm -rf {} \;
for f in $(find src/* -type d); do mv "$f" "`echo $f | sed s/Paradigm.Services/Paradigm.Services/`"; done
for f in $(find src/**/*.* -type f); do mv "$f" "`echo $f | sed s/Paradigm.Services/Paradigm.Services/`"; done
