var data = new Dictionary<string, string>()
{
    {"query", "{items(name: \"m855a1\") { id name shortName }}"}
};

using (var httpClient = new HttpClient())
{

    //Http response message
    var httpResponse = await httpClient.PostAsJsonAsync("https://api.tarkov.dev/graphql", data);

    //Response content
    var responseContent = await httpResponse.Content.ReadAsStringAsync();

    //Print response
    Debug.WriteLine(responseContent);

}