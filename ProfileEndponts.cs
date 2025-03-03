namespace ProfileAPI;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Update.Internal;

public static class ProfileEndpoints{
    public static void  MapProfileEndpoints(RouteGroupBuilder group){
        group.MapGet("/", async (string? firstName, string? lastName, string? email, string? phoneNumber, ProfileDb db) => {
            db.Profiles.Where(p => 
                (firstName == null) ? true : firstName == p.FirstName
                && (lastName == null) ? lastName == p.LastName : true
                && (email == null) ? lastName == p.Email : true
                && (phoneNumber == null) ? phoneNumber == p.PhoneNumber : true
            );
            return await db.Profiles.ToListAsync();
        });

        group.MapGet("/{id}", async (Guid id, ProfileDb db) => {
            return (await db.Profiles.FindAsync(id)) 
                is Profile profile
                ? Results.Ok(profile) 
                : Results.NotFound();
        });
        group.MapPost("/", async (Profile profile, ProfileDb db) => {
            List<Profile> collision = await db.Profiles.Where(p => p.Email == profile.Email).ToListAsync();
            if(collision.Any()){
                return Results.Conflict("This email address is already in use");
            }
            db.Profiles.Add(profile);
            await db.SaveChangesAsync();
            return Results.Created();
        });
        group.MapPut("/{id}", async(Guid id, Profile updatedProfile, ProfileDb db) => {
            Profile? profile = await db.Profiles.FindAsync(id);
            if(profile == null) return Results.NotFound();
            List<Profile> collision = await db.Profiles.Where(p => p.Email == updatedProfile.Email).ToListAsync();
            if(collision.Any() && collision[0].Email != profile.Email){
                return Results.Conflict("This email address is already in use");
            }
            profile.FirstName = updatedProfile.FirstName;
            profile.LastName = updatedProfile.LastName;
            profile.Email = updatedProfile.Email;
            profile.PhoneNumber = updatedProfile.PhoneNumber;
            await db.SaveChangesAsync();
            return Results.Created();
        }); 
        group.MapDelete("/{id}", async(Guid id, ProfileDb db) => {
            Profile? profile = await db.Profiles.FindAsync(id);
            if(profile == null) return Results.NotFound();
            db.Profiles.Remove(profile);
            await db.SaveChangesAsync();
            return Results.Ok();
        });

            
    }
}