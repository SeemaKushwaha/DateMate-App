import { Component, OnInit } from '@angular/core';
import { AuthService } from './_services/auth.service';
import { User } from './_models/user';

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
    const user: User = JSON.parse(localStorage.getItem('user'));
    if (token)
    {
      this.authservice.decodedToken = this.authservice.jwtHelper.decodeToken(token);
    }
    if (user)
    {
      this.authservice.currentUser = user;
      this.authservice.changeMemberPhoto(user.photoUrl);
    }
  }
}
