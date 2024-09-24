import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";

@Injectable({providedIn: 'root'})
export class LoginService {
    private baseUrl = 'https://localhost:44304/api/students';

    constructor(private http: HttpClient) {}

    existsByEmail(email: string) {
        return this.http.get<boolean>(`${this.baseUrl}/existsByEmail?email=${email}`);
    }
}