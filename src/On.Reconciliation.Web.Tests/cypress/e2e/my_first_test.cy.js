describe("Foo", () => {

    beforeEach(() => {
        // login logic here, after auth is implemented
    });

    it("successfully opens a page", () => {
        cy.visit("http://localhost:21080");
        cy.getByDataCy("list-element-2"); //ok
        cy.get(".container-one").getByDataCy("list-element-2"); //ok
        //cy.get(".container-two").getByDataCy("list-element-2"); // fails
    });

});