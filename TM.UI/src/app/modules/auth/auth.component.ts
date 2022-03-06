import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { BaseService } from '@app/modules/shared/services/base.service';

@Component({
  selector: 'app-auth',
  templateUrl: './auth.component.html',
  styleUrls: ['./auth.component.css']
})
export class AuthComponent implements OnInit {

  constructor(private baseService: BaseService, private router: Router) {
    this.goToHome();
  }

  ngOnInit(): void {
  }

  goToHome() {
    if(this.baseService.isLoggedIn) {   
		  this.router.navigate(['projects']).then();
    }
	}
}
