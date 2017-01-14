# 線上讀書會 - API Token 入門

文字版 - 文章介紹 [網址](http://columns.chicken-house.net/2016/12/01/microservice7-apitoken/)

範例程式 source code，及 PPT 都在這裡。

**PPT** 下載: [APITOKEN-DEMO.pptx](APITOKEN-DEMO.pptx?raw=true)  
**SLIDESHARE**: http://www.slideshare.net/chickenwu/api-token


展示用的兩個 web app, 我也在 Azure 上面佈署了一份。
要測試的可以直接連這邊:

- **AUTH**: 執行認證與授權  
http://andrewmeetupdemoauth.azurewebsites.net/swagger/

- **API**: 存取服務  
http://andrewmeetupdemoapi.azurewebsites.net/swagger/




### 產生 APIKEY 的指令

> C:\\> ApiKeyGenerator.exe AndrewWu VIP CTO MVP
```
jgAAAAJDbGllbnRJRAAJAAAAQW5kcmV3V3UABFRhZ3MAJgAAAAIwAAQAAABWSVAAAjEABAAAAENUTwACMgAEAAAATVZQAAACVHlwZU5hbWUAJAAAAE1lZXRVcC5BcGlUb2tlbkRlbW8uU0RLLkFwaUtleVRva2VuAAlFeHBpcmVEYXRlALpyu8NfAQAAAA==|X56LXzXG3sAJgzEz7RSMGdcWBDroHNdu+6gpXluhoP0JZAxurzgpYPrwZ64ycCyIv0xiYoAjSj8Afz3CGW6HL1O/3N6c2as7OPNYUgOD6MGvHw5KXaZQ0WK4Y44TQn3kRzk7+55UlwMM2/ztSzM0o/XkL/wqstLwrTU3EHX/PeY=
```

> C:\\> ApiKeyGenerator.exe AndrewWu
```
bQAAAAJDbGllbnRJRAAJAAAAQW5kcmV3V3UABFRhZ3MABQAAAAACVHlwZU5hbWUAJAAAAE1lZXRVcC5BcGlUb2tlbkRlbW8uU0RLLkFwaUtleVRva2VuAAlFeHBpcmVEYXRlALBPqMRfAQAAAA==|unZq3Np1y38a4zws4OMT+is8rIoysFi7AvaKHRERFc6HLFVcyrbK3hocrSGhLo8HbTgrf1lIwCa7Ix5EtRhtJknoovAZ/79D5V32RAJXdKpVccm+oeuUaF1FL0zfAk6hRnBrTHULV9QwN0csIcBjnlVVyucVQZ15N0B79z43mio=
```

> C:\\> ApiKeyGenerator.exe AndrewWu BAD
```
eAAAAAJDbGllbnRJRAAJAAAAQW5kcmV3V3UABFRhZ3MAEAAAAAIwAAQAAABCQUQAAAJUeXBlTmFtZQAkAAAATWVldFVwLkFwaVRva2VuRGVtby5TREsuQXBpS2V5VG9rZW4ACUV4cGlyZURhdGUAzV+pxF8BAAAA|s6PTwIN0YmTN2DzQ9qQRLsKXaX7cYRvsV9CeR7ggGWZ6j4Rv+6KCI6WdfPQDCRcmkoDXkzIJ1ydmuLgTvTAUbcfJtkIzIf8Fx8IK/pkV4/78bKAPt0sUZqSyP5sFc4bbLiLfZSJL0e1pvAleNMda1vpc1KaJ4+CbJTw+hY1jcWo=
```


