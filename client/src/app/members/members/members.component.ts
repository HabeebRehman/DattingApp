import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { NgxGalleryAnimation, NgxGalleryImage, NgxGalleryOptions } from '@kolkov/ngx-gallery';
import { Member } from '_Model/members';
import { MemberService } from '_Services/member.service';

@Component({
  selector: 'app-members',
  templateUrl: './members.component.html',
  styleUrls: ['./members.component.css']
})
export class MembersComponent implements OnInit {
  member:Member
  galleryOption:NgxGalleryOptions[];
  galleryImage:NgxGalleryImage[];
  constructor(private memberService :MemberService,private route:ActivatedRoute) { }

  ngOnInit(): void {
    this.loadMember();
    
    this.galleryOption=[
      {
        width:'500px',
        height:'500px',
        imagePercent:100,
        thumbnailsColumns:4,
        imageAnimation:NgxGalleryAnimation.Slide,
        preview:false
      }
    ];

    
  }

  GetImages():NgxGalleryImage[]
  {
     const ImageUrls=[];
   
    for(const foto of this.member.photos)
    {
      ImageUrls.push(
        {
          small:foto.url,
          medium:foto.url,
          large:foto.url
        }
      )
    }

     
     return ImageUrls;
  }

  loadMember()
  {
    console.log("user name get "+this.route.snapshot.paramMap.get("username"))
    this.memberService.getMember(this.route.snapshot.paramMap.get("username")).subscribe(user=>{
      this.member=user;
      this.galleryImage=this.GetImages();
    })
  }

}
