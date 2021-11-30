import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { Relations } from '../Models/relations.model';

@Component({
  selector: 'app-relations',
  templateUrl: './relations.component.html',
  styleUrls: ['./relations.component.css']
})
export class RelationsComponent implements OnInit {

  constructor(private http: HttpClient) {
    this.ShowAll();
  }
  RelationList: Array<Relations>;
  relation = new Relations();

  ngOnInit() {
  }

  ShowAll() {
    this.http.get<Array<Relations>>("https://localhost:44329/relation/getall").subscribe(x => this.RelationList = x);
  }
  AddRelation() {
    this.http.post("https://localhost:44329/relation/add", this.relation).subscribe(x => this.ShowAll());
  }
  Remove(id: number) {
    this.http.delete("https://localhost:44329/relation/" + id).subscribe(x => this.ShowAll());
  }
}
