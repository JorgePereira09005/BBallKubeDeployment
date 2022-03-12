package jmpc.PictureService.controllers

import jmpc.PictureService.response.StandardResponse
import org.springframework.beans.factory.annotation.Value
import org.springframework.http.HttpStatus
import org.springframework.http.ResponseEntity
import org.springframework.web.bind.annotation.GetMapping
import org.springframework.web.bind.annotation.PathVariable
import org.springframework.web.bind.annotation.RequestMapping
import org.springframework.web.bind.annotation.RestController
import java.net.HttpURLConnection
import java.net.URL

@RestController
@RequestMapping("/api/picture")
class PictureController(
//    @Value("\${bballservice.url}")
//    private val bballServiceUrl: String,
    @Value("\${basekwiki.url}")
    private val baseWikiUrl: String
) {

    @GetMapping("/{playerName}")
    fun getPictureUrl(@PathVariable playerName: String) : ResponseEntity<StandardResponse> {
        var url = URL(baseWikiUrl.plus(playerName).plus("_(basketball)"))
        var connection: HttpURLConnection = url.openConnection() as HttpURLConnection
        connection.requestMethod = "HEAD";

        if(connection.responseCode != HttpURLConnection.HTTP_OK) {
            url = URL(baseWikiUrl.plus(playerName))
            connection = url.openConnection() as HttpURLConnection

            if (connection.responseCode != HttpURLConnection.HTTP_OK) return ResponseEntity(null, HttpStatus.BAD_REQUEST)
        }

        return ResponseEntity<StandardResponse>(StandardResponse(PictureUrl = "", WikiUrl = url.toString()), HttpStatus.OK)
    }
}