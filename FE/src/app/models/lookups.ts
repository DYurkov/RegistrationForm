export class Lookups {
  referrers: ReferrerListOption[];
}

export class ListOption {
  id: number;
  name: string;
}

export class ReferrerListOption extends ListOption {
  canEnterManually: boolean;
}
