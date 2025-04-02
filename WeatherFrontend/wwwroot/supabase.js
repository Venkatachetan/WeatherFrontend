const supabaseUrl = 'https://cnmohzlxvibrjlnhrmss.supabase.co'; // Replace with your Supabase project URL
const supabaseKey = 'eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJpc3MiOiJzdXBhYmFzZSIsInJlZiI6ImNubW9oemx4dmlicmpsbmhybXNzIiwicm9sZSI6ImFub24iLCJpYXQiOjE3Mzk1MTM5MDYsImV4cCI6MjA1NTA4OTkwNn0.zX4R2Sen4YQeSKySqHOt4Ah_XYWwQUpQjxbDafYWBiI'; // Replace with your Supabase anon key
const supabase = supabase.createClient(supabaseUrl, supabaseKey);

// Listen for authentication state changes
supabase.auth.onAuthStateChange((event, session) => {
    if (event == 'SIGNED_IN' && session) {
        const accessToken = session.access_token;
        fetch(`/api/auth/google-login?accessToken=${encodeURIComponent(accessToken)}`)
            .then(response => response.json())
            .then(data => {
                localStorage.setItem('authToken', data.accessToken);
                localStorage.setItem('userId', data.id);
                localStorage.setItem('userEmail', data.email);
                window.location.href = '/dashboard';
            });
    }
});

// Function to initiate Google login
window.signInWithGoogle = () => {
    supabase.auth.signInWithOAuth({ provider: 'google' });
};