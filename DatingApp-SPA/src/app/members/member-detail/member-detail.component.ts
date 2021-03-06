import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { User } from 'src/app/_models/user';
import { AlertifyService } from 'src/app/_services/alertify.service';
import { UserService } from 'src/app/_services/user.service';
import {NgxGalleryOptions,NgxGalleryImage,NgxGalleryAnimation} from '@kolkov/ngx-gallery';

@Component({
  selector: 'app-member-detail',
  templateUrl: './member-detail.component.html',
  styleUrls: ['./member-detail.component.scss']
})
export class MemberDetailComponent implements OnInit {
  user: User;
  galleryOptions:NgxGalleryOptions[];
  galleryImages:NgxGalleryImage[];

  constructor(private userService:UserService,private alertify:AlertifyService,
    private Route:ActivatedRoute) { }

  ngOnInit() {
    this.Route.data.subscribe(data=>
      this.user=data['user']);

    this.galleryOptions=[
      {
        width:'500px',
        height:'500px',
        imagePercent:100,
        thumbnailsColumns:4,
        imageAnimation:NgxGalleryAnimation.Slide,
        preview:false
      }
    ];

    this.galleryImages=this.getImages();
  }

  getImages()
  {
    console.log(this.user);
    const imgUrls=[];
    for (const photo of this.user.photos ) {
      imgUrls.push({
        small:photo.url,
        medium:photo.url,
        large:photo.url,
        description:photo.description
      });
    }
    return imgUrls;
  }


 

}
