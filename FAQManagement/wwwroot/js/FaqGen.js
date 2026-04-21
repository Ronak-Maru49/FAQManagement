        const API_KEY = "AIzaSyCc8-5_T96AySKTpYy4oUWeOcqOuEn-6V4";
        const BASE_URL = "https://generativelanguage.googleapis.com/v1beta";

        const btn = document.getElementById('generateBtn');
        const input = document.getElementById('topicInput');
        const output = document.getElementById('faqResult');
        const loader = document.getElementById('loader');

        // Sabse pehle check karte hain aapke liye kaunse models available hain
        async function getAvailableModel() {
            const response = await fetch(`${BASE_URL}/models?key=${API_KEY}`);
        const data = await response.json();

        if (!data.models) throw new Error("API Key issue: No models found at all!");

            // Hum flash ya pro model dhundenge jo generateContent support karta ho
            const model = data.models.find(m =>
        m.supportedGenerationMethods.includes("generateContent") &&
        (m.name.includes("flash") || m.name.includes("pro"))
        );

        return model ? model.name : null;
        }

        async function generateFaq(topic) {
            const activeModel = await getAvailableModel();
        if (!activeModel) throw new Error("No compatible models found for this key.");

        console.log("Neko is using model:", activeModel);

        const prompt = {
            contents: [{parts: [{text: `You are a playful cat named Neko. Generate a 5-question FAQ about: ${topic}. Use cat puns.` }] }]
            };

        const response = await fetch(`${BASE_URL}/${activeModel}:generateContent?key=${API_KEY}`, {
            method: 'POST',
        headers: {'Content-Type': 'application/json' },
        body: JSON.stringify(prompt)
            });

        const data = await response.json();
        return data.candidates[0].content.parts[0].text;
        }

        btn.addEventListener('click', async () => {
            const topic = input.value.trim();
        if (!topic) return alert("Neko needs a topic!");

        btn.disabled = true;
        loader.style.display = 'block';
        output.style.display = 'none';

        try {
                const faqText = await generateFaq(topic);
        output.innerHTML = faqText.replace(/\n/g, '<br>').replace(/\*\*(.*?)\*\*/g, '<strong>$1</strong>');
            output.style.display = 'block';
            } catch (error) {
                console.error("Debug Error:", error);
            output.innerHTML = `
            <div class="alert alert-danger">
                <strong>🐾 Neko's Diagnostic Report:</strong><br>
                    <small>${error.message}</small><hr>
                        <p class="small">Bhai, Google aapki key ko models nahi dikha raha. <br>
                            <strong>Solution:</strong> <a href="https://aistudio.google.com/app/apikey" target="_blank">Yahan click karo</a> aur <strong>"Create API key in NEW project"</strong> button dabao. Nayi key 100% chalegi!</p>
                    </div>`;
                    output.style.display = 'block';
            } finally {
                        btn.disabled = false;
                    loader.style.display = 'none';
            }
        });