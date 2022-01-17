import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {
  registerMode = false;
  loginMode = false;

  constructor() { }

  ngOnInit(): void { 
  }

  registerToggle() {
    this.registerMode = !this.registerMode;
  }

  loginToggle() {
    this.loginMode = !this.loginMode;
  }

  cancelRegisterMode(event: boolean) {
    this.registerMode = event;
  }

  cancelLoginMode(event: boolean) {
    this.loginMode = event;
  }
}
