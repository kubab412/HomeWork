import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { WorkPlace } from '../Models/work-place.model';

@Component({
  selector: 'app-work-place',
  templateUrl: './work-place.component.html',
  styleUrls: ['./work-place.component.css']
})
export class WorkPlaceComponent implements OnInit {
  WorkPlaces: Array<WorkPlace>;
  workPlace = new WorkPlace();

  constructor(private http: HttpClient, private route: Router) {
    this.ShowAll();
  }

  ngOnInit() {
  }
  ShowAll() {
    this.http.get<Array<WorkPlace>>("https://localhost:44329/work-place/getall").subscribe(x => this.WorkPlaces = x);

  }
  AddWorkPlace() {
    this.http.post("https://localhost:44329/work-place/add", this.workPlace).subscribe(x => this.ShowAll());

  }
  Update(id: number, name: string, street: string, houseNumber: number, place: string, zipCode: number, voivodeship: string) {
    var edit = new WorkPlace()
    {
      edit.Id = id,
        edit.Name = name,
        edit.Street = street,
        edit.HouseNumber = houseNumber,
        edit.Place = place,
        edit.ZipCode = zipCode,
        edit.Voivodeship = voivodeship
    };
    this.http.put("https://localhost:44329/work-place/edit", edit).subscribe(x => this.ShowAll());
    
  }
  Remove(id: number) {
    this.http.delete("https://localhost:44329/work-place/" + id).subscribe(x => this.ShowAll());
   
  }
  Filter(name: string) {
    this.http.get<Array<WorkPlace>>("https://localhost:44329/work-place/filter/" + name).subscribe(x => this.WorkPlaces = x);
  }
}
