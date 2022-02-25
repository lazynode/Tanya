/*
package main

import (
	"net/http"
)

func init() {
	http.HandleFunc("/test.json", func(w http.ResponseWriter, r *http.Request) {
		http.Redirect(w, r, "https://127.0.0.1:8888", http.StatusFound)
	})
}
*/

await new Neo.Plugins.OracleHttpsProtocol().ProcessAsync(new System.Uri("https://${YOUR_SERVER_THAT_REDIRECT_TO_LOCAL_HOST}"), new CancellationToken());
