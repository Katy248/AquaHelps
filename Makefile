watch-css:
	cd AquaHelps.Blazor/ && \
        tailwindcss --watch -i App.css -o wwwroot/css/app.css --minify
        
watch-app:
	dotnet watch --project AquaHelps.Blazor