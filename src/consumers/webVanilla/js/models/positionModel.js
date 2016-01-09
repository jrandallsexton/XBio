var PositionModel = function(data) {
    this.Id = null;
    this.PersonId = null;
    this.CompanyId = null;
    this.TitleId = null;
    this.Details = [];
    this.StartDate = null;
    this.EndDate = null;
    this.Created = null;
    this.Modified = null;

    if ((data !== null) && (data !== undefined)) {
        this.Id = data.Id;
        this.PersonId = data.PersonId;
        this.CompanyId = data.CompanyId;
        this.TitleId = data.TitleId;
        this.Details = data.Details;
        this.StartDate = data.StartDate;
        this.EndDate = data.EndDate;
        this.Created = data.Created;
        this.Modified = data.Modified;
    }
};