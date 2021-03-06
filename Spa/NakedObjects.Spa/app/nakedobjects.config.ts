﻿// custom configuration for a particular implementation 

// path to Restful Objects server 
const appPath = "http://nakedobjectsrodemo.azurewebsites.net";
//const appPath = "http://localhost:61546";

export function getAppPath() {
    if (appPath.charAt(appPath.length - 1) === "/") {
        return appPath.length > 1 ? appPath.substring(0, appPath.length - 1) : "";
    }
    return appPath;
}

export const logoffUrl = appPath + "/Account/Logoff";

// this can be a full url eg http://www.google.com
export const postLogoffUrl = "/#/gemini/home";

export const defaultPageSize = 20; // can be overridden by server 
export const listCacheSize = 5;

export const shortCutMarker = "___";
export const urlShortCuts = ["http://nakedobjectsrodemo.azurewebsites.net", "AdventureWorksModel"];

export const keySeparator = "--";
export const objectColor = "object-color";
export const linkColor = "link-color";

export const autoLoadDirty = true;
export const showDirtyFlag = false || !autoLoadDirty;

// caching constants: do not change unless you know what you're doing 
export const httpCacheDepth = 50;
export const transientCacheDepth = 4;
export const recentCacheDepth = 20;

// checks for inconsistencies in url 
// deliberately off in .pp config file 
export const doUrlValidation = true;
