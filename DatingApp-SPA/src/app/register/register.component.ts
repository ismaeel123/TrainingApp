import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { AuthService } from '../_services/auth.service';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.scss']
})
export class RegisterComponent implements OnInit {
  @Output() cancelRegister = new EventEmitter();
  model:any={}
  constructor(private auth:AuthService) { }

  ngOnInit() {
  }

  register()
  {
    this.auth.register(this.model).subscribe(()=>{
      console.log("yalla mbrook");
    },error=>{
      console.log(error);
    })
  }

  cancel(){
    this.cancelRegister.emit(false);
    console.log('cancelled');
  }

}
