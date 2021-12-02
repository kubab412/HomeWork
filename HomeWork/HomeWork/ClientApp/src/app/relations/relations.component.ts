import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { Doctors } from '../Models/doctors.model';
import { Relations } from '../Models/relations.model';
import { WorkPlace } from '../Models/work-place.model';

@Component({
  selector: 'app-relations',
  templateUrl: './relations.component.html',
  styleUrls: ['./relations.component.css']
})
export class RelationsComponent implements OnInit {

  constructor(private http: HttpClient) {
    this.ShowAll();
    this.ShowDoctors();
    this.ShowWorkPlace();
  }
  RelationList: Array<Relations>;
  relation = new Relations();
  doctor = new Doctors();
  workPlace = new WorkPlace();
  DoctorList: Array<Doctors>;
  WorkPlaceList: Array<WorkPlace>;

  ngOnInit() {
  }

  ShowAll() {
    this.http.get<Array<Relations>>("https://localhost:44329/relation/getall").subscribe(x => this.RelationList = x);
  }
  ShowDoctors() {
    this.http.get<Array<Doctors>>("https://localhost:44329/doctors/getall").subscribe(x => this.DoctorList = x);
  }
  ShowWorkPlace() {
    this.http.get<Array<WorkPlace>>("https://localhost:44329/work-place/getall").subscribe(x => this.WorkPlaceList = x);
  }
  AddRelation(doctorId: number, workPlaceId: number) {
    var newRelation = new Relations()
    {
      newRelation.DoctorId = doctorId,
      newRelation.WorkPlaceId = workPlaceId
  };
    this.http.post("https://localhost:44329/relation/add", newRelation).subscribe(x => this.ShowAll());
  }
  Remove(id: number) {
    this.http.delete("https://localhost:44329/relation/" + id).subscribe(x => this.ShowAll());
  }
}
