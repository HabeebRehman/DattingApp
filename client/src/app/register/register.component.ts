import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { AbstractControl, FormBuilder, FormControl, FormGroup, ValidatorFn, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { AccountService } from '_Services/account.service';


@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {
  model:any={};
  // @Input() userFromHomeComponent:any;
  @Output() CancelRegister=new EventEmitter();
  registerForm:FormGroup;
  maxDate:Date;
  validationErrors:string[]=[];

  constructor(private accountService: AccountService,private toaster:ToastrService, private fb:FormBuilder, private router:Router) { 


  }

  ngOnInit(): void {
    this.initializeForm();
    this.maxDate=new Date();
    this.maxDate.setFullYear(this.maxDate.getFullYear()-18)
  }

  register()
  {
  
    this.accountService.Register(this.registerForm.value).subscribe(resp=>{
     this.router.navigateByUrl('/members');
    },error=>{
      this.validationErrors=error;

    });
    
  }

  initializeForm()
  {
    this.registerForm =  this.fb.group({
      gender:['male'],
      username:['',Validators.required],
      knownas:['',[Validators.required]],
      dateofbirth:['',[Validators.required]],
      city:['',[Validators.required]],
      country:['',[Validators.required]],
      password:['',[Validators.required,Validators.minLength(4),Validators.maxLength(8)]],
      confirmpassword:['',[Validators.required,this.matchValues('password')]]

    })

      this.registerForm.controls.password.valueChanges.subscribe(()=>{

      this.registerForm.controls.confirmpassword.updateValueAndValidity();
    })
  }

  cancel()
  {
    this.CancelRegister.emit(false);
  }
  matchValues(MatchTo:string):ValidatorFn
  {
    return (control:AbstractControl)=>
    {
      return control?.value===control?.parent?.controls[MatchTo].value?null:{ismatching:true}
    }
  }



}
