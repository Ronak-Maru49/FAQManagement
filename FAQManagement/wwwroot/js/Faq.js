        $(document).ready(function () {

            // 1. ADD MORE SECTIONS (Includes Sub-Category for every section)
            $('#addMoreBtn').click(function () {
                var newSection = `
                    <div class="faq-section" style="background: #f9f9f9; padding: 20px; border-radius: 8px; border: 1px solid #ddd; margin-bottom: 20px; position: relative;">
                        <button type="button" class="remove-section" style="position: absolute; top: 10px; right: 10px; background: red; color: white; border: none; border-radius: 50%; width: 25px; height: 25px; cursor: pointer;">&times;</button>
                        <h5>Additional FAQ Item</h5>

                        <div class="mb-3">
                            <label class="fw-bold">Sub Category</label>
                            <select class="subcategory-input form-control">
                                <option value="">-- Select Sub Category --</option>
                                <option value="General">General</option>
                                <option value="Technical">Technical</option>
                                <option value="Pricing">Pricing</option>
                            </select>
                        </div>

                        <div class="mb-3">
                            <label class="fw-bold">FAQ Title *</label>
                            <input type="text" class="question-input form-control" placeholder="Enter title" required />
                        </div>
                        <div class="mb-3">
                            <label class="fw-bold">FAQ Description *</label>
                            <textarea class="answer-input form-control" rows="3" placeholder="Enter description" required></textarea>
                        </div>
                    </div>`;
                $('#faqSectionsContainer').append(newSection);
            });

            // 2. REMOVE A SECTION
            $(document).on('click', '.remove-section', function () {
                $(this).closest('.faq-section').remove();
            });

            // 3. SUBMIT ALL SECTIONS
            $('#submitAllBtn').click(async function () {
                // Get the main category from the first section
                var selectedCategory = $('#mainCategory').val();

                if (!selectedCategory) {
                    $('#result').html('<span style="color:red;">Please select a Category in the first section.</span>');
                    return;
                }

                const sections = $('.faq-section');
                let successCount = 0;
                let errorOccurred = false;

                $('#result').html('<span>Saving...</span>');

                for (let section of sections) {
                    var faqData = {
                        question: $(section).find('.question-input').val(),
                        answer: $(section).find('.answer-input').val(),
                        category: selectedCategory,
                        // Pull the sub-category from THIS specific section
                        subCategory: $(section).find('.subcategory-input').val()
                    };

                    if (!faqData.question || !faqData.answer) continue;

                    try {
                        await $.ajax({
                            url: '/api/Faq',
                            type: 'POST',
                            contentType: 'application/json',
                            data: JSON.stringify(faqData)
                        });
                        successCount++;
                    } catch (err) {
                        errorOccurred = true;
                    }
                }

                if (successCount > 0) {
                    $('#result').html('<span style="color:green;">Successfully added ' + successCount + ' FAQs!</span>');
                    $('.question-input, .answer-input, .subcategory-input').val('');
                    $('.faq-section:not(:first)').remove();
                }

                if (errorOccurred) {
                    $('#result').append('<br/><span style="color:red;">Error saving some items.</span>');
                }
            });
        });