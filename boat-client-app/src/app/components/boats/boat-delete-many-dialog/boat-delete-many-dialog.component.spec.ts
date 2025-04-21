import { ComponentFixture, TestBed } from '@angular/core/testing';

import { BoatDeleteManyDialogComponent } from './boat-delete-many-dialog.component';

describe('BoatDeleteManyDialogComponent', () => {
  let component: BoatDeleteManyDialogComponent;
  let fixture: ComponentFixture<BoatDeleteManyDialogComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [BoatDeleteManyDialogComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(BoatDeleteManyDialogComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
