import { Component, OnDestroy, OnInit } from '@angular/core';
import { AbstractControl, FormBuilder, FormGroup, ValidationErrors, ValidatorFn, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { ReferrerListOption } from '@app/models';
import { AccountService, AlertService } from '@app/services';
import { Subject } from 'rxjs';
import { first, pairwise, startWith, takeUntil } from 'rxjs/operators';


@Component({ templateUrl: 'register.component.html' })
export class RegisterComponent implements OnInit, OnDestroy {
  d$: Subject<any>;
  form: FormGroup;
  referrers: ReferrerListOption[] = [];
  loading = false;
  submitted = false;
  canShowManualReferrerInput = false;

  constructor(
    private formBuilder: FormBuilder,
    private route: ActivatedRoute,
    private router: Router,
    private accountService: AccountService,
    private alertService: AlertService
  ) {
    this.d$ = new Subject<void>();
    const f = this.ngOnDestroy.bind(this);
    this.ngOnDestroy = () => {
      f();
      this.d$.next(null);
      this.d$.complete();
    };
  }

  ngOnInit() {
    this.form = this.formBuilder.group({
      firstName: ['', Validators.required],
      lastName: ['', Validators.required],
      username: ['', [Validators.required, Validators.email]],
      password: ['', [Validators.required, Validators.minLength(6)]],
      registrationReferrerId: ['', [Validators.required]],
      registrationReferrerCustomText: ['', [this.customUserRegistrationValidator()]]
    });

    this.accountService.getLookups()
      .pipe(first())
      .subscribe(lookups => this.referrers = lookups ? lookups.referrers : []);

    this.form.get('registrationReferrerId')
      .valueChanges
      .pipe(takeUntil(this.d$),
        startWith(null),
        pairwise())
      .subscribe(([prev, next]: [number, number]) => {
        this.canShowManualReferrerInput = this.getCanShowManualReferrerInput(next);
        this.form.controls['registrationReferrerCustomText'].updateValueAndValidity();
      });
  }

  get f() { return this.form.controls; }

  onSubmit() {
    this.submitted = true;

    this.alertService.clear();

    if (this.form.invalid) {
      return;
    }

    this.loading = true;
    this.accountService.register(this.form.value)
      .pipe(first())
      .subscribe({
        next: () => {
          this.alertService.success('Registration successful', { keepAfterRouteChange: true });
          this.router.navigate(['../login'], { relativeTo: this.route });
        },
        error: error => {
          this.alertService.error(error);
          this.loading = false;
        }
      });
  }

  ngOnDestroy(): void { }

  customUserRegistrationValidator(): ValidatorFn {
    return (control: AbstractControl): ValidationErrors | null => {
      if (!control.parent) {
        return null;
      }
      const registrationReferrerId = control.parent.get('registrationReferrerId').value;
      const customReferrerText = control.parent.get('registrationReferrerCustomText').value;
      const canShowManualReferrerInput = this.getCanShowManualReferrerInput(registrationReferrerId);
      return canShowManualReferrerInput && !customReferrerText ? { registrationReferrerCustomText: { value: control.value } } : null;
    };
  }

  private getCanShowManualReferrerInput(newValue: number): boolean {
    return (this.referrers || []).some(x => x.id === newValue && x.canEnterManually);
  }
}
