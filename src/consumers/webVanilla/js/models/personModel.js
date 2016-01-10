var PersonModel = function(data) {
    this.Id = null;
    this.LastName = null;
    this.FirstName = null;
    this.MiddleName = null;
    this.Display = null;
    this.Address = null;
    this.Positions = [];
    this.Skills = [];

    if ((data !== null) && (data !== undefined)) {
        this.Id = data.Id;
        this.LastName = data.LastName;
        this.FirstName = data.FirstName;
        this.MiddleName = data.MiddleName;
        this.Display = data.LastName;
        this.Address = data.Address;
        this.Positions = data.Positions;
        this.Skills = data.Skills;
    }
};