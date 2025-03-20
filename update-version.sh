#!/bin/bash

VERSION="$1"

sed "s/.*private static readonly string VERSION .*/        private static readonly string VERSION = \"$VERSION\";/g" Packages/com.appylar.unity-sdk/Runtime/AppylarSdkWrapper/Appylar.cs > Packages/com.appylar.unity-sdk/Runtime/AppylarSdkWrapper/Appylar.cs.tmp
mv Packages/com.appylar.unity-sdk/Runtime/AppylarSdkWrapper/Appylar.cs.tmp Packages/com.appylar.unity-sdk/Runtime/AppylarSdkWrapper/Appylar.cs
