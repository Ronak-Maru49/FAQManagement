        $(document).ready(function () {
            loadFaqs();

            // Handle Update Submit
            $('#faqForm').submit(function (e) {
                e.preventDefault();

                var id = $('#faqId').val();
                var faqData = {
                    id: parseInt(id),
                    question: $('#Question').val(),
                    answer: $('#Answer').val()
                };

                $.ajax({
                    url: '/api/Faq/' + id,
                    type: 'PUT',
                    contentType: 'application/json',
                    data: JSON.stringify(faqData),
                    success: function () {
                        $('#result').html('<span style="color:green;">FAQ updated successfully!</span>');
                        cancelEdit();
                        loadFaqs();
                    },
                    error: function (xhr) {
                        $('#result').html('<span style="color:red;">Error updating FAQ.</span>');
                    }
                });
            });
        });

        // 1. GET ALL FAQs
        function loadFaqs() {
            $.ajax({
                url: '/api/Faq',
                type: 'GET',
                success: function (data) {
                    var rows = '';
                    $.each(data, function (index, item) {
                        rows += '<tr>';
                        rows += '<td><strong>' + item.question + '</strong></td>';
                        rows += '<td>' + item.answer + '</td>';
                        rows += '<td style="text-align:center;">' +
                                '<button onclick="editFaq(' + item.id + ')" style="background: #007bff; color: white; border:none; padding:5px 10px; cursor:pointer; border-radius:3px; margin-right: 5px;">Edit</button>' +
                                '<button onclick="deleteFaq(' + item.id + ')" style="background: #dc3545; color: white; border:none; padding:5px 10px; cursor:pointer; border-radius:3px;">Delete</button>' +
                                '</td>';
                        rows += '</tr>';
                    });
                    $('#faqTableBody').html(rows);
                }
            });
        }

        // 2. OPEN EDIT SECTION
        function editFaq(id) {
            $.ajax({
                url: '/api/Faq/' + id,
                type: 'GET',
                success: function (faq) {
                    $('#faqId').val(faq.id);
                    $('#Question').val(faq.question);
                    $('#Answer').val(faq.answer);

                    $('#editSection').slideDown(); // Show the edit form
                    $('#result').empty();
                    window.scrollTo({ top: 0, behavior: 'smooth' });
                }
            });
        }

        // 3. DELETE FAQ
        function deleteFaq(id) {
            if (confirm("Are you sure you want to delete this FAQ?")) {
                $.ajax({
                    url: '/api/Faq/' + id,
                    type: 'DELETE',
                    success: function () {
                        $('#result').html('<span style="color:blue;">FAQ deleted.</span>');
                        loadFaqs();
                        if($('#faqId').val() == id) cancelEdit(); // Hide form if deleted item was being edited
                    }
                });
            }
        }

        function cancelEdit() {
            $('#editSection').slideUp();
            $('#faqForm')[0].reset();
            $('#faqId').val("0");
        }