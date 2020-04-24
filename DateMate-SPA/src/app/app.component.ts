import { Component, OnInit } from '@angular/core';
import { AuthService } from './_services/auth.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {
  title = 'DateMate App';
  constructor(private authservice: AuthService) {

  }
  ngOnInit(): void {
    const token = localStorage.getItem('token');
    if (token)
    {
      this.authservice.decodedToken = this.authservice.jwtHelper.decodeToken(token);
    }
  }
}
